namespace BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

public sealed class GetPermissionsByLoginTaskPermission(string permissionCode, GetPermissionsByLoginTaskRestriction? restrictions)
{
    public string PermissionCode { get; } = permissionCode;

    public GetPermissionsByLoginTaskRestriction? Restrictions { get; } = restrictions;
}