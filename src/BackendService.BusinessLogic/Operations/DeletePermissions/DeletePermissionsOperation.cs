using BackendService.BusinessLogic.Operations.DeletePermissions.Models;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.DeletePermissions;

public sealed class DeletePermissionsOperation(IAuthorizationServiceDbContext context, ILogger<DeletePermissionsOperation> logger) : IDeletePermissionsOperation
{
    public async Task DeletePermissionsAsync(DeletePermissionsOperationRequest request)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.Login == request.Login);

        if (user == null)
            return;
        
        context.UserRoles.RemoveRange(context.UserRoles.Where(ur => ur.UserId == user.UserId));
        context.UserRestrictions.RemoveRange(context.UserRestrictions.Where(ur => ur.UserId == user.UserId));

        await context.SaveChangesAsync(CancellationToken.None);
    }
}