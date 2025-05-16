using AuthService.Shared.Common;
using AuthService.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Shared;

public static class SharedDependencyInjection
{
    public static IServiceCollection AddSharedDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration);

        services.AddHttpContextAccessor();

        services.AddScoped<ClaimService>();

        return services;
    }
}
