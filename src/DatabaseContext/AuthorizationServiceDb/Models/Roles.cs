namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class Roles
{
    public int RoleId { get; set; }
    
    public required string RoleName { get; set; }

    public required string RoleCode { get; set; }
    
    public required string? Description { get; set; }
}