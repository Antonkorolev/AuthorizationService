using BackendService.BusinessLogic.Operations.AddPermissionsToUser;
using BackendService.BusinessLogic.Operations.DeletePermissions;
using BackendService.BusinessLogic.Operations.GetPermissions;
using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using BackendService.Contracts.Request;
using BackendService.Contracts.Response;
using BackendService.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
public sealed class PermissionController(
    IGetPermissionsOperation getPermissionsOperation,
    IAddPermissionsToUserOperation addPermissionsToUserOperation,
    IDeletePermissionsOperation deletePermissionsOperation) : ControllerBase
{
    [HttpGet("ActionAvailable")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionAvailableResponse> ActionAvailableAsync(ActionAvailableRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpGet("GetPermissions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetPermissionsResponse> GetPermissionsAsync(GetPermissionsRequest request)
    {
        var getPermissionsOperationResponse = await getPermissionsOperation.GetAsync(new GetPermissionsOperationRequest(request.Login));

        return getPermissionsOperationResponse.Map();
    }

    [HttpPost("AddPermissionsToUser")]
    public async Task AddPermissionsAsync(AddPermissionsToUserRequest request)
    {
        await addPermissionsToUserOperation.AddPermissionsAsync(request.Map());
    }

    [HttpPost("DeletePermissions")]
    public async Task DeletePermissionsAsync(DeletePermissionsRequest request)
    {
        await deletePermissionsOperation.DeletePermissionsAsync(request.Map());
    }
}