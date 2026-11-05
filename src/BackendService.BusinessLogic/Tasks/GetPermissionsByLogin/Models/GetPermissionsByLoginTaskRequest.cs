namespace BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

public sealed class GetPermissionsByLoginTaskRequest(string login)
{
    public string Login { get; } = login;
}