using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using BackendService.Contracts.Request;
using AddPermissionsToUserRoleInfo = BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models.RoleInfo;

namespace BackendService.Mappers;

public static class RequestMapper
{
    public static AddPermissionsOperationRequest Map(this AddPermissionsToUserRequest addPermissionsToUserRequest)
    {
        var rolesInfo = addPermissionsToUserRequest.RoleInfos.Select(ri => new AddPermissionsToUserRoleInfo(ri.RoleCode, ri.RestrictionTypeCode, ri.RestrictionValue));

        return new AddPermissionsOperationRequest(addPermissionsToUserRequest.Login, rolesInfo);
    }
}