namespace BackendService.Contracts.Request;

public sealed class AddUserRequest(string login, string role, string restrictionTypeCode, string restrictionValue)
{
    public string Login { get; set; } = login;

    public string Role { get; set; } = role;

    public string RestrictionTypeCode { get; set; } = restrictionTypeCode;

    public string RestrictionValue { get; set; } = restrictionValue;
}