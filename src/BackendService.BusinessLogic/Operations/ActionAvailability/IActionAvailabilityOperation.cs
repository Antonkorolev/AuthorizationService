namespace BackendService.BusinessLogic.Operations.ActionAvailability;

public interface IActionAvailabilityOperation
{
    Task<bool> ExecuteAsync(string login, string permissionCode, string restrictionType, string? restrictionValue);
}