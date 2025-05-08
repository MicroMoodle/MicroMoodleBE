using AuthService.Core.Repositories;
using AuthService.DataAccess.Persistence;
using AuthService.Infrastructure.Repositories;
using AuthService.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructuresDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfiguration>>().Value.ConnectionStrings;
        services.AddDbContext<AuthDatabaseContext>(options =>
            options.UseNpgsql(connectionStrings.PostgreSQL));

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
