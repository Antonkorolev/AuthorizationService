namespace BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

public sealed class GetPermissionsByLoginTaskRestriction(string? restrictionTypeCode, string? restrictionValue)
{
    public string? RestrictionTypeCode { get; set; } = restrictionTypeCode;

    public string? RestrictionValue { get; set; } = restrictionValue;
}