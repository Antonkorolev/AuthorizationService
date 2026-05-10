namespace BackendService.Contracts.Request;

public sealed class DeletePermissionsRequest(string login, RoleInfo[] roleInfos)
{
    public string Login { get; set; } = login;

    public RoleInfo[] RoleInfos { get; set; } = roleInfos;
}