using BackendService.Contracts.Request;
using BackendService.Contracts.Response;

namespace BackendService.Contracts;

public interface IAuthorizationService
{
    Task<ActionAvailableResponse> ActionAvailableAsync(ActionAvailableRequest request);

    Task<GetPermissionsResponse> GetPermissionsAsync(GetPermissionsResponse request);

    Task AddPermissionsAsync(AddPermissionsToUserRequest toUserRequest);

    Task DeletePermissionsAsync(DeletePermissionsRequest request);
}