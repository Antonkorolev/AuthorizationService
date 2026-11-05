namespace BackendService.BusinessLogic.Operations.CanRead;

public sealed class CanReadOperation : ICanReadOperation
{
    public Task<bool> ExecuteAsync(string login, string restrictionType, string restrictionValue)
    {
        throw new NotImplementedException();
    }
}