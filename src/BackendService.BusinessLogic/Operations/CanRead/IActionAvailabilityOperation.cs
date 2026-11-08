namespace BackendService.BusinessLogic.Operations.CanRead;

public interface IActionAvailabilityOperation
{
    Task<bool> ExecuteAsync(string login, string action, string restrictionType, string? restrictionValue);
}