using BackendService.BusinessLogic.Operations.AddPermissionsToUser;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.AddUserRestrictions;
using BackendService.BusinessLogic.Operations.AddPermissionsToUser.Tasks.EnsureUserExistsTask;
using BackendService.BusinessLogic.Operations.DeletePermissionsFromUser;
using BackendService.BusinessLogic.Operations.GetPermissions;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOperations(this IServiceCollection services)
    {
        services.AddTransient<IEnsureUserExistsTask, EnsureUserExistsTask>();
        services.AddTransient<IAddUserPermissionsTask, AddUserPermissionsTask>();
        services.AddTransient<IAddPermissionsToUserOperation, AddPermissionsToUserOperation>();

        services.AddTransient<IGetPermissionsOperation, GetPermissionsOperation>();
        services.AddTransient<IDeletePermissionsFromUserOperation, DeletePermissionsFromUserOperation>();
        
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