using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.ActionAvailability;
using BackendService.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ActionsController(IActionAvailabilityOperation actionAvailabilityOperation) : ControllerBase
{
    [HttpGet("read")]
    public async Task<bool> ReadAsync(string login, int entityId, ResourceType resourceType)
    {
        return await actionAvailabilityOperation.ExecuteAsync(login, PermissionCode.Read, resourceType.ToString(), entityId.ToString());
    }
    
    [HttpGet("write")]
    public async Task<bool> WriteAsync(string login, int? entityId, ResourceType resourceType)
    {
        return await actionAvailabilityOperation.ExecuteAsync(login, PermissionCode.Write, resourceType.ToString(), entityId.ToString());
    }
    
    [HttpGet("update")]
    public async Task<bool> UpdateAsync(string login, int entityId, ResourceType resourceType)
    {
        return await actionAvailabilityOperation.ExecuteAsync(login, PermissionCode.Update, resourceType.ToString(), entityId.ToString());
    }

    [HttpGet("delete")]
    public async Task<bool> DeleteAsync(string login, int entityId, ResourceType resourceType)
    {
        return await actionAvailabilityOperation.ExecuteAsync(login, PermissionCode.Delete, resourceType.ToString(), entityId.ToString());
    }
}