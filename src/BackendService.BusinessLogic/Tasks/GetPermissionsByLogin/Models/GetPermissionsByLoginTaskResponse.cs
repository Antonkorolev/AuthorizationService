namespace BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

public sealed class GetPermissionsByLoginTaskResponse(IEnumerable<GetPermissionsByLoginTaskRolePermission> rolePermissions)
{
    public IEnumerable<GetPermissionsByLoginTaskRolePermission> RolePermissions { get; } = rolePermissions;
}