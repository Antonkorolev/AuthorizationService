namespace DatabaseContext.AuthorizationServiceDb.Models;

public sealed class RestrictionType
{
    public int RestrictionTypeId { get; set; }
    
    public required string RestrictionTypeName { get; set; }
    
    public required string RestrictionTypeCode { get; set; }
    
    public string? Description { get; set; }
}