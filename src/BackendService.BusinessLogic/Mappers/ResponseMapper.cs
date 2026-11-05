using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

namespace BackendService.BusinessLogic.Mappers;

public static class ResponseMapper
{
    public static GetPermissionsOperationResponse Map(this GetPermissionsByLoginTaskResponse response)
    {
        var rolePermissions = response.RolePermissions
            .Select(rp => new RolePermission(rp.RoleCode, rp.Permissions
                .Select(p => new Permission(p.PermissionCode, p.Restrictions != null ? new Restriction(p.Restrictions.RestrictionTypeCode, p.Restrictions.RestrictionValue) : null))));

        return new GetPermissionsOperationResponse(rolePermissions ?? []);
    }
}