using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using BackendService.Contracts.Response;
using Permission = BackendService.Contracts.Response.Permission;
using Restriction = BackendService.Contracts.Response.Restriction;
using RolePermission = BackendService.Contracts.Response.RolePermission;

namespace BackendService.Mappers;

public static class ResponsesMapper
{
    public static GetPermissionsResponse Map(this GetPermissionsOperationResponse getPermissionsOperationResponse)
    {
        var rolePermissions = getPermissionsOperationResponse.RolePermissions
            .Select(rp => new RolePermission(rp.RoleCode, rp.Permissions
                .Select(p => new Permission(p.PermissionCode, p.Restrictions != null ? new Restriction(p.Restrictions.RestrictionTypeCode, p.Restrictions.RestrictionValue) : null))));

        return new GetPermissionsResponse(rolePermissions);
    }
}