using BackendService.BusinessLogic.Operations.ActionAvailability;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser;
using BackendService.BusinessLogic.Operations.GetPermissions;
using BackendService.BusinessLogic.Tasks.GetPermissionsByLogin;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOperations(this IServiceCollection services)
    {
        services.AddCommonTask();

        services.AddGetPermissionsOperation();
        services.AddPermissionsToUserOperation();
        services.AddDeletePermissionsFromUserOperation();
        services.AddActionAvailabilityOperation();
        
        return services;
    }

    private static IServiceCollection AddGetPermissionsOperation(this IServiceCollection services)
    {
        services.AddTransient<IGetPermissionsOperation, GetPermissionsOperation>();

        return services;
    }

    private static IServiceCollection AddDeletePermissionsFromUserOperation(this IServiceCollection services)
    {
        services.AddTransient<IDeletePermissionsFromUserOperation, DeletePermissionsFromUserOperation>();

        return services;
    }

    private static IServiceCollection AddPermissionsToUserOperation(this IServiceCollection services)
    {
        services.AddTransient<IEnsureUserExistsTask, EnsureUserExistsTask>();
        services.AddTransient<IAddUserPermissionsTask, AddUserPermissionsTask>();
        services.AddTransient<IAddPermissionsToUserOperation, AddPermissionsToUserOperation>();

        return services;
    }

    private static IServiceCollection AddActionAvailabilityOperation(this IServiceCollection services)
    {
        services.AddTransient<IActionAvailabilityOperation, ActionAvailabilityOperation>();

        return services;
    }

    private static IServiceCollection AddCommonTask(this IServiceCollection services)
    {
        services.AddTransient<IGetPermissionsByLoginTask, GetPermissionsByLoginTask>();

        return services;
    }

    public static IServiceCollection AddUserDbContext(this IServiceCollection services, string name, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(name);

        services.AddDbContext<AuthorizationServiceDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>());
            })
            .AddScoped<IAuthorizationServiceDbContext>(x => x.GetRequiredService<AuthorizationServiceDbContext>());

        return services;
    }
}