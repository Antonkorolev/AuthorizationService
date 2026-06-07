namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;

public sealed class AddUserRestrictionsTaskRequest(int userId, IEnumerable<AddUserRestrictionsTaskRoleInfo> roleInfos)
{
    public int UserId { get; set; } = userId;
    
    public IEnumerable<AddUserRestrictionsTaskRoleInfo> RoleInfos { get; } = roleInfos;
}