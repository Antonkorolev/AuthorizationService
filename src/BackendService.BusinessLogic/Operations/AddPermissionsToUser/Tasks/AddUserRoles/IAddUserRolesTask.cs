namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRoles;

public interface IAddUserRolesTask
{
    Task AddAsync(int userId, IEnumerable<string> roleCodes);
}