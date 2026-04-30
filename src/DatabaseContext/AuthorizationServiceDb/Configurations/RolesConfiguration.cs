using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseContext.AuthorizationServiceDb.Configurations;

public sealed class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("Roles", "dbo");
        builder.HasKey(r => r.RoleId);

        builder.Property(r => r.RoleName).HasMaxLength(50).IsRequired();
        builder.Property(r => r.RoleCode).HasMaxLength(50).IsRequired();
        builder.Property(r => r.Description).HasMaxLength(256);
    }
}