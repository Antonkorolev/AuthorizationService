using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models;
using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models;
using BackendService.Contracts.Request;
using RoleInfo = BackendService.BusinessLogic.Operations.DeletePermissionsFromUser.Models.RoleInfo;
using AddPermissionsToUserRoleInfo = BackendService.BusinessLogic.Operations.AddPermissionsToUser.Models.RoleInfo;

namespace BackendService.Mappers;

public static class RequestMapper
{
    public static DeletePermissionsOperationRequest Map(this DeletePermissionsFromUserRequest deletePermissionsFromUserRequest)
    {
        var roleInfos = deletePermissionsFromUserRequest.RoleInfos.Select(ri => new RoleInfo(ri.RoleCode, ri.RestrictionTypeCode, ri.RestrictionValue));
            
        return new DeletePermissionsOperationRequest(deletePermissionsFromUserRequest.Login, roleInfos);
    }

    public static AddPermissionsOperationRequest Map(this AddPermissionsToUserRequest addPermissionsToUserRequest)
    {
        var rolesInfo = addPermissionsToUserRequest.RoleInfos.Select(ri => new AddPermissionsToUserRoleInfo(ri.RoleCode, ri.RestrictionTypeCode, ri.RestrictionValue));

        return new AddPermissionsOperationRequest(addPermissionsToUserRequest.Login, rolesInfo);
    }
}