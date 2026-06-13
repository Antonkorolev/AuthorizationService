namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;

public sealed class AddUserPermissionsTaskRequest(int userId, IEnumerable<AddUserPermissionsTaskRoleInfo> roleInfos)
{
    public int UserId { get; set; } = userId;
    
    public IEnumerable<AddUserPermissionsTaskRoleInfo> RoleInfos { get; } = roleInfos;
}