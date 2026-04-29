using DatabaseContext.AuthorizationServiceDb.Configurations;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.AuthorizationServiceDb;

public sealed class AuthorizationServiceDb : DbContext, IAuthorizationServiceDb
{
    public DbSet<Users> User { get; set; }
    public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<RolePermissions> RolePermissions { get; set; }
    public DbSet<Permissions> Permissions { get; set; }
    
    public AuthorizationServiceDb(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UsersConfiguration());
        builder.ApplyConfiguration(new UserRolesConfiguration());
        builder.ApplyConfiguration(new RolesConfiguration());
        builder.ApplyConfiguration(new RolePermissionsConfiguration());
        builder.ApplyConfiguration(new PermissionsConfiguration());
    }
}