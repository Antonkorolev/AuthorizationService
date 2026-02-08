namespace BackendService.Contracts.Response;

public sealed class ActionAvailableResponse(bool available)
{
    public bool Available { get; set; } = available;
}