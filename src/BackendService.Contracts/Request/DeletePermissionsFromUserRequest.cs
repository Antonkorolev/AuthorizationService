namespace BackendService.Contracts.Request;

public sealed class DeletePermissionsFromUserRequest(string login, IEnumerable<RoleInfo> roleInfos)
{
    public string Login { get; } = login;

    public IEnumerable<RoleInfo> RoleInfos { get; } = roleInfos;
}