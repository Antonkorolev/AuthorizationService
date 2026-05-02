namespace BackendService.BusinessLogic.Operations.GetPermissions.Models;

public sealed class GetPermissionsOperationRequest(string login)
{
    public string Login { get; } = login;
}