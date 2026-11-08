using BackendService.BusinessLogic.Operations.CanRead;
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
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        
        return await actionAvailabilityOperation.ExecuteAsync(login, actionName, resourceType.ToString(), entityId.ToString());
    }
    
    [HttpGet("write")]
    public async Task<bool> WriteAsync(string login, int? entityId, ResourceType resourceType)
    {
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        
        return await actionAvailabilityOperation.ExecuteAsync(login, actionName, resourceType.ToString(), entityId.ToString());
    }
    
    [HttpGet("update")]
    public async Task<bool> UpdateAsync(string login, int entityId, ResourceType resourceType)
    {
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        
        return await actionAvailabilityOperation.ExecuteAsync(login, actionName, resourceType.ToString(), entityId.ToString());
    }

    [HttpGet("delete")]
    public async Task<bool> DeleteAsync(string login, int entityId, ResourceType resourceType)
    {
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        
        return await actionAvailabilityOperation.ExecuteAsync(login, actionName, resourceType.ToString(), entityId.ToString());
    }
}