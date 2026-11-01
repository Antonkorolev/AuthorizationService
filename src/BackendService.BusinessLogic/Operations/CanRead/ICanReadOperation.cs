using BackendService.BusinessLogic.Models;

namespace BackendService.BusinessLogic.Operations.CanRead;

public interface ICanReadOperation
{
    Task<bool> ExecuteAsync(string login, int entityId, ResourceType resourceType);
}