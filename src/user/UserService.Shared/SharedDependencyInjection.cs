using UserService.Shared.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Shared;

public static class SharedDependencyInjection
{
    public static IServiceCollection AddSharedDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration);

        services.AddHttpContextAccessor();

        return services;
    }
}
