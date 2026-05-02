using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseContext.AuthorizationServiceDb.Configurations;

public sealed class UserRestrictionsConfiguration : IEntityTypeConfiguration<UserRestrictions>
{
    public void Configure(EntityTypeBuilder<UserRestrictions> builder)
    {
        builder.ToTable("UserRestrictions", "dbo");
        builder.HasKey(ur => ur.UserRestrictionId);

        builder.Property(ur => ur.RestrictionValue).HasMaxLength(50);
    }
}