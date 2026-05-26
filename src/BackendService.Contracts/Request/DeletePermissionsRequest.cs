namespace BackendService.Contracts.Request;

public sealed class DeletePermissionsRequest(string login, RoleInfo[] roleInfos)
{
    public string Login { get; } = login;

    public RoleInfo[] RoleInfos { get; } = roleInfos;
}