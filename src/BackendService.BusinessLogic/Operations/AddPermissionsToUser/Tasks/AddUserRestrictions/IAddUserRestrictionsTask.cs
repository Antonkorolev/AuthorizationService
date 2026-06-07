using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;

public interface IAddUserRestrictionsTask
{
    Task AddAsync(AddUserRestrictionsTaskRequest request);
}