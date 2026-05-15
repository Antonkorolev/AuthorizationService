using BackendService.BusinessLogic.Operations.AddPermissions.Models;

namespace BackendService.BusinessLogic.Operations.AddPermissions;

public interface IAddPermissionsOperation
{
    Task AddPermissionsAsync(AddPermissionsOperationRequest request);
}