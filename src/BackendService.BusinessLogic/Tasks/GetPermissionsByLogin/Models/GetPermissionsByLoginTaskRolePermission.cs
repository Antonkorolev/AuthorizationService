namespace BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

public sealed class GetPermissionsByLoginTaskRolePermission(string role, IEnumerable<GetPermissionsByLoginTaskPermission> permissions)
{
    public string RoleCode { get; } = role;

    public IEnumerable<GetPermissionsByLoginTaskPermission> Permissions { get; } = permissions;
}