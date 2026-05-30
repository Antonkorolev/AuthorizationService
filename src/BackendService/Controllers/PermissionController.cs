using BackendService.BusinessLogic.Operations.AddPermissionsToUser;
using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser;
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
    IDeletePermissionsFromUserOperation deletePermissionsFromUserOperation) : ControllerBase
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
        await addPermissionsToUserOperation.AddAsync(request.Map());
    }

    [HttpPost("DeletePermissionsFromUser")]
    public async Task DeletePermissionsFromUserAsync(DeletePermissionsFromUserRequest fromUserRequest)
    {
        await deletePermissionsFromUserOperation.DeleteAsync(fromUserRequest.Map());
    }
}