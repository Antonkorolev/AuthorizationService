namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class UserRestrictions
{
    public int UserRestrictionId { get; set; }
    
    public int UserId { get; set; }
    
    public int RoleId { get; set; }
    
    public int PermissionId { get; set; }
    
    public int RestrictionTypeId { get; set; }
    
    public string? RestrictionValue { get; set; }
}