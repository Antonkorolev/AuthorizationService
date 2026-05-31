using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRoles;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser;

public sealed class AddPermissionsToUserOperation(IAuthorizationServiceDbContext context, IEnsureUserExistsTask ensureUserExistsTask, IAddUserRolesTask addUserRolesTask, ILogger<AddPermissionsToUserOperation> logger) : IAddPermissionsToUserOperation
{
    public async Task AddAsync(AddPermissionsOperationRequest request)
    {
        await context.Database.BeginTransactionAsync();
        
        try
        {
            var userId = await ensureUserExistsTask.ExecuteAsync(request.Login);
            
            var roleCodes = request.RoleInfos.Select(ri => ri.RoleCode).ToArray();

            await addUserRolesTask.AddAsync(userId, roleCodes);
            
            // var rolesPermissions = await context.RolePermissions
            //     .Where(rp => roleIds.Contains(rp.RoleId))
            //     .Join(context.Permissions, rp => rp.PermissionId, p => p.PermissionId, (rp, p) => new { rp.RoleId, p.PermissionId })
            //     .ToArrayAsync();

            // foreach (var rolesPermission in rolesPermissions)
            // {
            //     await context.UserRestrictions.AddAsync(new UserRestrictions { UserId = user.UserId });
            // }

            //await context.SaveChangesAsync(CancellationToken.None);
                
            await context.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await context.Database.RollbackTransactionAsync();
            throw;
        }
    }
}