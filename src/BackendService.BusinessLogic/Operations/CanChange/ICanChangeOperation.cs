using BackendService.BusinessLogic.Models;

namespace BackendService.BusinessLogic.Operations.CanChange;

public interface ICanChangeOperation
{
    Task<bool> ExecuteAsync(string login, int entityId, ResourceType resourceType);
}