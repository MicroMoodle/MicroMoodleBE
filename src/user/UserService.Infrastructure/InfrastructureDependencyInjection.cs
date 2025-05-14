using UserService.Core.Repositories;
using UserService.DataAccess.Persistence;
using UserService.Infrastructure.Repositories;
using UserService.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace UserService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructuresDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfiguration>>().Value.ConnectionStrings;
        services.AddDbContext<UserDatabaseContext>(options =>
            options.UseNpgsql(connectionStrings.PostgreSQL));

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
