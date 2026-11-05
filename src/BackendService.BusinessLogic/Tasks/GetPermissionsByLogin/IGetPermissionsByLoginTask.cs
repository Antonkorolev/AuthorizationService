using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;

namespace BackendService.BusinessLogic.Tasks.GetPermissionsByLogin;

public interface IGetPermissionsByLoginTask
{
    Task<GetPermissionsByLoginTaskResponse> GetAsync(GetPermissionsByLoginTaskRequest request);
}