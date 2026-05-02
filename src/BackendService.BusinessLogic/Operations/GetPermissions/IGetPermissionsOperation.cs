using BackendService.BusinessLogic.Operations.GetPermissions.Models;

namespace BackendService.BusinessLogic.Operations.GetPermissions;

public interface IGetPermissionsOperation
{
    Task<GetPermissionsOperationResponse> GetAsync(GetPermissionsOperationRequest request);
}