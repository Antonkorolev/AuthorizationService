using BackendService.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ActionsController : ControllerBase
{
    Task<bool> CanReadAsync(string login, int entityId, ResourceType resourceType)
    {
        throw new NotImplementedException();
    }
    
    Task<bool> CanWriteAsync(string login, int? entityId, ResourceType resourceType)
    {
        throw new NotImplementedException();
    }
    
    Task<bool> CanChangeAsync(string login, int entityId, ResourceType resourceType)
    {
        throw new NotImplementedException();
    }

    Task<bool> CanDeleteAsync(string login, int entityId, ResourceType resourceType)
    {
        throw new NotImplementedException();
    }
}