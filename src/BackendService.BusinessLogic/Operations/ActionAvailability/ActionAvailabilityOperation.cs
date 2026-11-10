using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

namespace BackendService.BusinessLogic.Operations.ActionAvailability;

public sealed class ActionAvailabilityOperation(IGetPermissionsByLoginTask getPermissionsByLoginTask) : IActionAvailabilityOperation
{
    public async Task<bool> ExecuteAsync(string login, string permissionCode, string restrictionType, string? restrictionValue)
    {
        var response = await getPermissionsByLoginTask.GetAsync(new GetPermissionsByLoginTaskRequest(login));

        var permission = response.RolePermissions.Select(rp => rp.Permissions.Where(p => p.PermissionCode == permissionCode)).ToArray().SingleOrDefault();

        if (restrictionValue != null)
            return permission?.Select(p => p.Restrictions?.RestrictionTypeCode == restrictionType && p.Restrictions.RestrictionValue == restrictionValue).FirstOrDefault() != null;

        return permission?.FirstOrDefault() != null;
    }
}