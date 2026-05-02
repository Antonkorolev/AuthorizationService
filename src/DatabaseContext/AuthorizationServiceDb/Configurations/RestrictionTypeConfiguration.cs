using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseContext.AuthorizationServiceDb.Configurations;

public sealed class RestrictionTypeConfiguration : IEntityTypeConfiguration<RestrictionType>
{
    public void Configure(EntityTypeBuilder<RestrictionType> builder)
    {
        builder.ToTable("RestrictionType", "dbo");
        builder.HasKey(rt => rt.RestrictionTypeId);

        builder.Property(rt => rt.RestrictionTypeCode).HasMaxLength(50).IsRequired();
        builder.Property(rt => rt.RestrictionTypeName).HasMaxLength(50).IsRequired();
        builder.Property(rt => rt.Description).HasMaxLength(256);
    }
}