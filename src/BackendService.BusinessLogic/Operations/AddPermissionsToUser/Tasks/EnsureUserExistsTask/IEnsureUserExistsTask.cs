namespace BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;

public interface IEnsureUserExistsTask
{
   Task<int> ExecuteAsync(string login);
}