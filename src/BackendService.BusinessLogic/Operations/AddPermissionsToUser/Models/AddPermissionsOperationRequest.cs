namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;

public sealed class AddPermissionsOperationRequest(string login, IEnumerable<RoleInfo> roleInfos)
{
    public string Login { get; } = login;

    public IEnumerable<RoleInfo> RoleInfos { get; } = roleInfos;
}