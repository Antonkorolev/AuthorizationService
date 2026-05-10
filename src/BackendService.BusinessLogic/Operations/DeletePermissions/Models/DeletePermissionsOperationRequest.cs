namespace BackendService.BusinessLogic.Operations.DeletePermissions.Models;

public sealed class DeletePermissionsOperationRequest(string login, RoleInfo[] roleInfos)
{
    public string Login { get; set; } = login;

    public RoleInfo[] RoleInfos { get; set; } = roleInfos;
}