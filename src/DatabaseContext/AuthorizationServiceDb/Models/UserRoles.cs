namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class UserRoles
{
    public int UserRoleId { get; set; }
    
    public int UserId { get; set; }

    public int RoleId { get; set; }
}