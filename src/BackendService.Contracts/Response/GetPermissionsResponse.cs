namespace BackendService.Contracts.Response;

public sealed class GetPermissionsResponse(string[] permissions)
{
    public string[] Permissions { get; } = permissions;
}