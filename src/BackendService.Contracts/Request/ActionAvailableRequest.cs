namespace BackendService.Contracts.Request;

public sealed class ActionAvailableRequest(string login, string permission, int groupId)
{
    public string Login { get; set; } = login;

    public string Permission { get; set; } = permission;

    public int GroupId { get; set; } = groupId;
}