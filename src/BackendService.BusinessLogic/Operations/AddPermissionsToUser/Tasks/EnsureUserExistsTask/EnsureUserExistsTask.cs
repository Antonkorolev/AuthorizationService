using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;

public sealed class EnsureUserExistsTask(IAuthorizationServiceDbContext context) : IEnsureUserExistsTask
{
    public async Task<int> ExecuteAsync(string login)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.Login == login);

        if (user is not null) 
            return user.UserId;
        
        user = new Users { Login = login };
        await context.User.AddAsync(user);
        await context.SaveChangesAsync(CancellationToken.None);

        return user.UserId;
    }
}