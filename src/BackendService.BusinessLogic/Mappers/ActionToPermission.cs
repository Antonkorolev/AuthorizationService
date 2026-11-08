using BackendService.BusinessLogic.Constants;

namespace BackendService.BusinessLogic.Mappers;

public static class ActionToPermission
{
    private static Dictionary<string, string> Mapper = new()
    {
        { "Read", Permission.Read },
        { "Write", Permission.Write },
        { "Change", Permission.Update },
        { "Delete", Permission.Delete },
        { "Share", Permission.Share }
    };

    public static string Get(string action)
    {
        return Mapper[action];
    }
}