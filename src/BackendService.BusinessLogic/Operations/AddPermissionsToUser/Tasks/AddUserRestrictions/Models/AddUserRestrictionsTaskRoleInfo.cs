namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;

public sealed class AddUserRestrictionsTaskRoleInfo(string roleCode, string? restrictionTypeCode, string? restrictionValue)
{
    public string RoleCode { get; set; } = roleCode;

    public string? RestrictionTypeCode { get; set; } = restrictionTypeCode;

    public string? RestrictionValue { get; set; } = restrictionValue;
}