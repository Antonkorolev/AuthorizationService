using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser;

public sealed class AddPermissionsToUserOperation(IAuthorizationServiceDbContext context, IEnsureUserExistsTask ensureUserExistsTask, IAddUserPermissionsTask addUserPermissionsTask, ILogger<AddPermissionsToUserOperation> logger) : IAddPermissionsToUserOperation
{
    public async Task AddAsync(AddPermissionsOperationRequest request)
    {
        await context.Database.BeginTransactionAsync();
        
        try
        {
            var userId = await ensureUserExistsTask.ExecuteAsync(request.Login);
            
            var addUserRestrictionsTaskRoleInfos = request.RoleInfos.Select(ri => new AddUserPermissionsTaskRoleInfo(ri.RoleCode, ri.RestrictionTypeCode, ri.RestrictionValue)).ToArray();

            await addUserPermissionsTask.AddAsync(new AddUserPermissionsTaskRequest(userId, addUserRestrictionsTaskRoleInfos));
            
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