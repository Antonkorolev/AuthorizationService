using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.AuthorizationServiceDb;

public interface IAuthorizationServiceDb : IDataContext
{
    public DbSet<Users> User { get; set; }
    
    public DbSet<UserRoles> UserRoles { get; set; }
    
    public DbSet<Roles> Roles { get; set; }
    
    public DbSet<RolePermissions> RolePermissions { get; set; }
    
    public DbSet<Permissions> Permissions { get; set; }
}