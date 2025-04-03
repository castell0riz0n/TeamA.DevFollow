using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamA.DevFollow.API.Database.Contexts;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await using ApplicationIdentityDbContext applicationIdentityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

        try
        {
            await applicationDbContext.Database.MigrateAsync();
            app.Logger.LogInformation("ApplicationDbContext migrations applied successfully.");

            await applicationIdentityDbContext.Database.MigrateAsync();
            app.Logger.LogInformation("ApplicationIdentityDbContext migrations applied successfully.");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "An error occurred while applying database migrations.");
            throw;
        }
    }

    public static async Task SeedDataAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        try
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                var role = new IdentityRole(Roles.Admin);
                await roleManager.CreateAsync(role);
                app.Logger.LogInformation("Admin role created successfully.");
            }
            if (!await roleManager.RoleExistsAsync(Roles.Member))
            {
                var role = new IdentityRole(Roles.Member);
                await roleManager.CreateAsync(role);
                app.Logger.LogInformation("Member role created successfully.");
            }
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "An error occurred while seeding the database.");
            throw;
        }
    }
}
