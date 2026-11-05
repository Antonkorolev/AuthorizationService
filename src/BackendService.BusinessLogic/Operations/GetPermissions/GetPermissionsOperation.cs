using BackendService.BusinessLogic.Mappers;
using BackendService.BusinessLogic.Operations.GetPermissions.Models;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin.Models;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetPermissions;

public sealed class GetPermissionsOperation(GetPermissionsByLoginTask getPermissionsByLoginTask, ILogger<GetPermissionsOperation> logger) : IGetPermissionsOperation
{
    public async Task<GetPermissionsOperationResponse?> GetAsync(GetPermissionsOperationRequest request)
    {
        var response = await getPermissionsByLoginTask.GetAsync(new GetPermissionsByLoginTaskRequest(request.Login));

        logger.LogInformation("Permissions for Login = '{login}' successfully received", request.Login);
        
        return response.Map();
    }
}