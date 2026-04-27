namespace BackendService.Contracts.Request;

public sealed class ActionAvailableRequest(string login, string permission, int groupId)
{
    public string Login { get; } = login;

    public string Permission { get; } = permission;

    public int GroupId { get; } = groupId;
}