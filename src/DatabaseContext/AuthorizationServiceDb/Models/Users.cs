namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class Users
{
    public int UserId { get; set; }
    
    public required string Login { get; set; }
}