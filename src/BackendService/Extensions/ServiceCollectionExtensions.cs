using BackendService.BusinessLogic.Operations.DeletePermissions;
using BackendService.BusinessLogic.Operations.GetPermissions;
using DatabaseContext.AuthorizationServiceDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOperations(this IServiceCollection services)
    {
        services.AddTransient<IGetPermissionsOperation, GetPermissionsOperation>();
        services.AddTransient<IDeletePermissionsOperation, DeletePermissionsOperation>();
        
        return services;
    }

    public static IServiceCollection AddUserDbContext(this IServiceCollection services, string name, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(name);

        services.AddDbContext<AuthorizationServiceDb>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>());
            })
            .AddScoped<IAuthorizationServiceDb>(x => x.GetRequiredService<AuthorizationServiceDb>());

        return services;
    }
}