namespace BackendService.BusinessLogic.Operations.CanDelete;

public interface ICanDeleteOperation
{
    Task<bool> ExecuteAsync(string login, string restrictionType, string restrictionValue);
}