namespace BackendService.BusinessLogic.Operations.GetPermissions.Models;

public sealed class GetPermissionsOperationResponse(IEnumerable<RolePermission> rolePermissions)
{
    public IEnumerable<RolePermission> RolePermissions { get; } = rolePermissions;
}