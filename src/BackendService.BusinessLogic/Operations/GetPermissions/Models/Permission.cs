namespace BackendService.BusinessLogic.Operations.GetPermissions.Models;

public sealed class Permission(string permissionCode, Restriction? restrictions)
{
    public string PermissionCode { get; } = permissionCode;

    public Restriction? Restrictions { get; } = restrictions;
}