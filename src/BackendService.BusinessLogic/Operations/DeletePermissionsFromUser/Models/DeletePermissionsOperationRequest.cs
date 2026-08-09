namespace BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models;

public sealed class DeletePermissionsOperationRequest(string login)
{
    public string Login { get; set; } = login;
}