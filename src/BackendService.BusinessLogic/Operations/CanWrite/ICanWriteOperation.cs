using BackendService.BusinessLogic.Models;

namespace BackendService.BusinessLogic.Operations.CanWrite;

public interface ICanWriteOperation
{
    Task<bool> ExecuteAsync(string login, int entityId, ResourceType resourceType);
}