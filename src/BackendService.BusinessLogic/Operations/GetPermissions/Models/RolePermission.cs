namespace BackendService.BusinessLogic.Operations.GetPermissions.Models;

public sealed class RolePermission(string role, IEnumerable<Permission> permissions)
{
    public string RoleCode { get; } = role;

    public IEnumerable<Permission> Permissions { get; } = permissions;
}