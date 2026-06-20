using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;

public sealed class AddUserPermissionsTask(IAuthorizationServiceDbContext context, ILogger<AddUserPermissionsTask> logger) : IAddUserPermissionsTask
{
    public async Task AddAsync(AddUserPermissionsTaskRequest request)
    {
        //RestrictionTypes? restrictionType = null;
        
        try
        {
            var roleCodes = request.RoleInfos.Select(ri => ri.RoleCode).ToArray();

            var rolesPermissions = await context.Roles
                .Where(r => roleCodes.Contains(r.RoleCode))
                .Join(context.RolePermissions,
                    r => r.RoleId,
                    rp => rp.RoleId,
                    (r, rp) => new { r.RoleId, rp.PermissionId, r.RoleCode })
                .ToArrayAsync();

            foreach (var roleInfo in request.RoleInfos)
            {
                var filteredRolesPermissions = rolesPermissions.Where(rp => rp.RoleCode == roleInfo.RoleCode).ToArray();

                if (filteredRolesPermissions.Length == 0)
                    throw new Exception($"Role not found by code = {roleInfo.RoleCode}");

                //if (roleInfo.RestrictionTypeCode != null) 
                var restrictionType = await context.RestrictionTypes.FirstOrDefaultAsync(rt => rt.RestrictionTypeCode == roleInfo.RestrictionTypeCode);
                
                await context.UserRoles.AddAsync(new UserRoles { UserId = request.UserId, RoleId = filteredRolesPermissions.First().RoleId });

                foreach (var filteredRolesPermission in filteredRolesPermissions)
                {
                    await context.UserRestrictions.AddAsync(
                        new UserRestrictions
                        {
                            UserId = request.UserId,
                            RoleId = filteredRolesPermission.RoleId,
                            PermissionId = filteredRolesPermission.PermissionId,
                            RestrictionTypeId = restrictionType?.RestrictionTypeId,
                            RestrictionValue = roleInfo.RestrictionValue
                        });
                }
            }

            await context.SaveChangesAsync(CancellationToken.None);
            
            logger.LogInformation("User permissions successfully added to user");
        }
        catch (Exception)
        {
            logger.LogInformation("User permission");
            throw;
        }
    }
}