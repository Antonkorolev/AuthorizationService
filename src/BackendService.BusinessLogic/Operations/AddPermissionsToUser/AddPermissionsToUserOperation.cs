using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser;

public sealed class AddPermissionsToUserOperation(IAuthorizationServiceDbContext context, ILogger<AddPermissionsToUserOperation> logger) : IAddPermissionsToUserOperation
{
    public async Task AddAsync(AddPermissionsOperationRequest request)
    {
        await context.Database.BeginTransactionAsync();
        try
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
            {
                user = new Users { Login = request.Login };
                await context.User.AddAsync(user);
                await context.SaveChangesAsync(CancellationToken.None); 
            }
            
            var roleCodes = request.RoleInfos.Select(ri => ri.RoleCode).ToArray();
            var roles = await context.Roles.Where(r => roleCodes.Contains(r.RoleCode)).ToArrayAsync();

            foreach (var role in roles)
            {
                await context.UserRoles.AddAsync(new UserRoles { UserId = user.UserId, RoleId = role.RoleId });
            }

            await context.SaveChangesAsync(CancellationToken.None);
                
            await context.Database.CommitTransactionAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
        }
        finally
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}