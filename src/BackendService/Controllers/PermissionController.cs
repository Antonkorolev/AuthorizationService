using BackendService.Contracts.Request;
using BackendService.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
public sealed class PermissionController : ControllerBase
{
    [HttpGet("ActionAvailable")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionAvailableResponse> ActionAvailable(ActionAvailableRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("GetPermissions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetPermissionsResponse> GetToken(GetPermissionsRequest request)
    {
        throw new NotImplementedException();
    }
}