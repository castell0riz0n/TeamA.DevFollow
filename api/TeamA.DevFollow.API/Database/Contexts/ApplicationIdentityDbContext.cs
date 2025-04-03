﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.Database.Contexts;

public class ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options): IdentityDbContext(options)
{

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Schemas.Identity);
        builder.Entity<IdentityUser>().ToTable("asp_net_users");
        builder.Entity<IdentityRole>().ToTable("asp_net_roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("asp_net_user_roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("asp_net_role_claims");
        builder.Entity<IdentityUserClaim<string>>().ToTable("asp_net_user_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("asp_net_user_logins");
        builder.Entity<IdentityUserToken<string>>().ToTable("asp_net_user_tokens");

        builder.Entity<RefreshToken>(a =>
        {
            a.HasKey(b => b.Id);
            a.Property(b => b.UserId).HasMaxLength(300);
            a.Property(b => b.Token).HasMaxLength(1000);
            a.HasIndex(b => b.Token).IsUnique();

            a.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
