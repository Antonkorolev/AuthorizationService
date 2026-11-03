namespace BackendService.BusinessLogic.Operations.CanRead;

public interface ICanReadOperation
{
    Task<bool> ExecuteAsync(string login, string restrictionType, string restrictionValue);
}