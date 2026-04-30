using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseContext.AuthorizationServiceDb.Configurations;

public sealed class PermissionsConfiguration : IEntityTypeConfiguration<Permissions>
{
    public void Configure(EntityTypeBuilder<Permissions> builder)
    {
        builder.ToTable("Permissions", "dbo");
        builder.HasKey(p => p.PermissionId);

        builder.Property(p => p.PermissionName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.PermissionCode).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(256);
    }
}