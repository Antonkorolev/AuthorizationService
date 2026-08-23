namespace BackendService.Contracts.Request;

public sealed class DeletePermissionsFromUserRequest(string login)
{
    public string Login { get; } = login;
}