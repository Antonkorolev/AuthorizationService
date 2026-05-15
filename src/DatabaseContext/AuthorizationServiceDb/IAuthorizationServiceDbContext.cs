using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.AuthorizationServiceDb;

public interface IAuthorizationServiceDbContext : IDataContext
{
    public DbSet<Users> User { get; set; }
    
    public DbSet<UserRoles> UserRoles { get; set; }
    
    public DbSet<Roles> Roles { get; set; }
    
    public DbSet<RolePermissions> RolePermissions { get; set; }
    
    public DbSet<Permissions> Permissions { get; set; }
    
    public DbSet<UserRestrictions> UserRestrictions { get; set; } 
    
    public DbSet<RestrictionTypes> RestrictionTypes { get; set; }
}