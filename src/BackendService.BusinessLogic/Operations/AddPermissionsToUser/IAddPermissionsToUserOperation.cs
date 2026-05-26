using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser;

public interface IAddPermissionsToUserOperation
{
    Task AddPermissionsAsync(AddPermissionsOperationRequest request);
}