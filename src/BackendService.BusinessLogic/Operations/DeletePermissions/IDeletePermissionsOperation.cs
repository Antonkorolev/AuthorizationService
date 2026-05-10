using BackendService.BusinessLogic.Operations.DeletePermissions.Models;

namespace BackendService.BusinessLogic.Operations.DeletePermissions;

public interface IDeletePermissionsOperation
{
    Task DeletePermissionsAsync(DeletePermissionsOperationRequest request);
}