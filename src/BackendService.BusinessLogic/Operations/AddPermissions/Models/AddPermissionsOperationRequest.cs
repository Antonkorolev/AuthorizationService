using BackendService.BusinessLogic.Operations.DeletePermissions.Models;

namespace BackendService.BusinessLogic.Operations.AddPermissions.Models;

public sealed class AddPermissionsOperationRequest(string login, RoleInfo[] roleInfos)
{
    public string Login { get; set; } = login;

    public RoleInfo[] RoleInfos { get; set; } = roleInfos;
}