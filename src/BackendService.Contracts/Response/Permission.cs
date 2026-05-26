namespace BackendService.Contracts.Response;

public sealed class Permission(string permissionCode, Restriction? restrictions)
{
    public string PermissionCode { get; } = permissionCode;

    public Restriction? Restrictions { get; } = restrictions;
}