namespace BackendService.BusinessLogic.Operations.CanWrite;

public interface ICanWriteOperation
{
    Task<bool> ExecuteAsync(string login, string restrictionType, string? restrictionValue);
}