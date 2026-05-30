using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models;

namespace BackendService.BusinessLogic.Operations.DeletePermissionsFromUser;

public interface IDeletePermissionsFromUserOperation
{
    Task DeleteAsync(DeletePermissionsOperationRequest request);
}