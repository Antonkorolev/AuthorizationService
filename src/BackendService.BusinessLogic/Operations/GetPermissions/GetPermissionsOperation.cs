using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetPermissions;

public sealed class GetPermissionsOperation(IAuthorizationServiceDbContext context, ILogger<GetPermissionsOperation> logger) : IGetPermissionsOperation
{
    public async Task<GetPermissionsOperationResponse?> GetAsync(GetPermissionsOperationRequest request)
    {
        GetPermissionsOperationResponse? getPermissionsOperationResponse = null;

        try
        {
            var rolePermissions = await context.User
                .Where(u => u.Login == request.Login)
                .Join(context.UserRoles, u => u.UserId, ur => ur.UserId, (u, ur) => new { u.UserId, ur.RoleId })
                .Join(context.Roles, ur => ur.RoleId, r => r.RoleId, (ur, r) => new { ur.UserId, ur.RoleId, r.RoleCode })
                .Join(context.RolePermissions, ur => ur.RoleId, rp => rp.RoleId, (r, rp) => new { r.UserId, r.RoleId, r.RoleCode, rp.PermissionId })
                .Join(context.Permissions, p => p.PermissionId, rp => rp.PermissionId, (rp, p) => new { rp.UserId, rp.RoleId, rp.RoleCode, rp.PermissionId, p.PermissionCode })
                .GroupJoin(context.UserRestrictions, p => new { p.UserId, p.RoleId, p.PermissionId }, ur => new { ur.UserId, ur.RoleId, ur.PermissionId }, (p, ur) => new { p, ur})
                .SelectMany(group => group.ur.DefaultIfEmpty(), (group, ur) => new { group.p.RoleCode, group.p.PermissionCode, RestrictionTypeId = ur == null ? null : ur.RestrictionTypeId, RestrictionValue = ur == null ? null : ur.RestrictionValue })
                .GroupJoin(context.RestrictionTypes, ur => ur.RestrictionTypeId, rt => rt.RestrictionTypeId, (ur, rt) => new { ur, rt })
                .SelectMany(group => group.rt.DefaultIfEmpty(), (group, rt) => new { group.ur.RoleCode, group.ur.PermissionCode, RestrictionTypeCode = rt == null ? null : rt.RestrictionTypeCode, group.ur.RestrictionValue })
                .ToArrayAsync();

            return new GetPermissionsOperationResponse(rolePermissions
                .GroupBy(r => r.RoleCode)
                .Select(rp => new RolePermission(rp.Key, rp
                    .GroupBy(p => p.PermissionCode)
                    .Select(pg => new Permission(pg.Key, pg
                        .Where(p => !string.IsNullOrEmpty(p.RestrictionTypeCode) && p.RestrictionValue != null)
                        .Select(p => new Restriction(p.RestrictionTypeCode, p.RestrictionValue))
                        .FirstOrDefault()
                    ))
                    .ToArray()
                )));
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
        }

        return getPermissionsOperationResponse;
    }
}