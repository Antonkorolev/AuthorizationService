using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions.Models;

namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;

public interface IAddUserPermissionsTask
{
    Task AddAsync(AddUserPermissionsTaskRequest request);
}