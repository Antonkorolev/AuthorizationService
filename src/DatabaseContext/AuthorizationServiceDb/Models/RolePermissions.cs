namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class RolePermissions
{
    public int RolePermissionId { get; set; }
    
    public int RoleId { get; set; }
    
    public int PermissionId { get; set; }
}