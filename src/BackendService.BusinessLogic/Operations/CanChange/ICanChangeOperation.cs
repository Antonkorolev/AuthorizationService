namespace BackendService.BusinessLogic.Operations.CanChange;

public interface ICanChangeOperation
{
    Task<bool> ExecuteAsync(string login, string restrictionType, string restrictionValue);
}