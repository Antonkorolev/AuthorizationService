using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.DeletePermissionsFromUser;

public sealed class DeletePermissionsFromUserOperation(IAuthorizationServiceDbContext context, ILogger<DeletePermissionsFromUserOperation> logger) : IDeletePermissionsFromUserOperation
{
    public async Task DeleteAsync(DeletePermissionsOperationRequest request)
    {
        await context.Database.BeginTransactionAsync();
        try
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user == null)
                return;

            context.UserRoles.RemoveRange(context.UserRoles.Where(ur => ur.UserId == user.UserId));
            context.UserRestrictions.RemoveRange(context.UserRestrictions.Where(ur => ur.UserId == user.UserId));

            await context.SaveChangesAsync(CancellationToken.None);
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