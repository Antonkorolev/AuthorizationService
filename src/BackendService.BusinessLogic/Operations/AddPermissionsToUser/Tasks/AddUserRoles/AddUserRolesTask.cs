using DatabaseContext.AuthorizationServiceDb;
using DatabaseContext.AuthorizationServiceDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRoles;

public sealed class AddUserRolesTask(IAuthorizationServiceDbContext context) : IAddUserRolesTask
{
    public async Task AddAsync(int userId, IEnumerable<string> roleCodes)
    {
        var roleIds = await context.Roles.Where(r => roleCodes.Contains(r.RoleCode)).Select(r => r.RoleId).ToArrayAsync();

        foreach (var roleId in roleIds)
        {
            await context.UserRoles.AddAsync(new UserRoles { UserId = userId, RoleId = roleId });
        }

        await context.SaveChangesAsync(CancellationToken.None);
    }
}