using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.Database.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasMaxLength(500);

        builder.Property(t => t.Name).HasMaxLength(100);

        builder.Property(t => new { t.Email }).HasMaxLength(300);
        builder.Property(t => new { t.IdentityId }).HasMaxLength(500);

        builder.HasIndex(t => new { t.Email }).IsUnique();
        builder.HasIndex(t => new { t.IdentityId }).IsUnique();
    }
}
