using BackendService.BusinessLogic.Models;

namespace BackendService.BusinessLogic.Operations.CanDelete;

public interface ICanDeleteOperation
{
    Task<bool> ExecuteAsync(string login, int entityId, ResourceType resourceType);
}