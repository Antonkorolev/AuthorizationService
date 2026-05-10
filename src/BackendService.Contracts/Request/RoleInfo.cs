namespace BackendService.Contracts.Request;

public sealed class RoleInfo(string roleCode, string? restrictionTypeCode, string? restrictionValue)
{
    public string RoleCode { get; set; } = roleCode;

    public string? RestrictionTypeCode { get; set; } = restrictionTypeCode;

    public string? RestrictionValue { get; set; } = restrictionValue;
}