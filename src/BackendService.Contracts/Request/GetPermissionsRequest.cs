namespace BackendService.Contracts.Request;

public sealed class GetPermissionsRequest(string login)
{
    public string Login { get; } = login;
}