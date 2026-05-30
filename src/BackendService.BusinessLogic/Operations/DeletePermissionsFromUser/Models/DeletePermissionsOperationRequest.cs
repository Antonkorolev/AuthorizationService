namespace BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models;

public sealed class DeletePermissionsOperationRequest(string login, IEnumerable<RoleInfo> roleInfos)
{
    public string Login { get; set; } = login;

    public IEnumerable<RoleInfo> RoleInfos { get; set; } = roleInfos;
}