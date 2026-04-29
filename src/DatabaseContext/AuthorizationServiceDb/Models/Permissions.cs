namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class Permissions
{
    public int PermissionId { get; set; }
    
    public required string PermissionName { get; set; }
    
    public required string PermissionCode { get; set; }
    
    public required string Description { get; set; }
}