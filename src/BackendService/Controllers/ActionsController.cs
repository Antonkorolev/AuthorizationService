using BackendService.BusinessLogic.Operations.CanChange;
using BackendService.BusinessLogic.Operations.CanDelete;
using BackendService.BusinessLogic.Operations.CanRead;
using BackendService.BusinessLogic.Operations.CanWrite;
using BackendService.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ActionsController(
    ICanReadOperation canReadOperation,
    ICanWriteOperation canWriteOperation, 
    ICanChangeOperation canChangeOperation, 
    ICanDeleteOperation canDeleteOperation) : ControllerBase
{
    [HttpGet("CanRead")]
    public async Task<bool> CanReadAsync(string login, int entityId, ResourceType resourceType)
    {
        return await canReadOperation.ExecuteAsync(login, resourceType.ToString(), entityId.ToString());
    }
    
    [HttpGet("CanWrite")]
    public async Task<bool> CanWriteAsync(string login, int? entityId, ResourceType resourceType)
    {
        return await canWriteOperation.ExecuteAsync(login, resourceType.ToString(), entityId?.ToString());
    }
    
    [HttpGet("CanChange")]
    public async Task<bool> CanChangeAsync(string login, int entityId, ResourceType resourceType)
    {
        return await canChangeOperation.ExecuteAsync(login, resourceType.ToString(), entityId.ToString());
    }

    [HttpGet("CanDelete")]
    public async Task<bool> CanDeleteAsync(string login, int entityId, ResourceType resourceType)
    {
        return await canDeleteOperation.ExecuteAsync(login, resourceType.ToString(), entityId.ToString());
    }
}