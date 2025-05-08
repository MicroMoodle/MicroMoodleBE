using AuthService.API.Common;

namespace AuthService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguration(configuration);

        return services;
    }

    private static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration);

        return services;
    }
}
