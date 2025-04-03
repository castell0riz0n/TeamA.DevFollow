using Microsoft.EntityFrameworkCore;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<HabitTag> HabitTags { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Application);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
