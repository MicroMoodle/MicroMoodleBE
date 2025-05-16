using AuthService.Application.Common.Interfaces;
using AuthService.Application.Common.Specifications;
using AuthService.DataAccess.Persistence;
using AuthService.Infrastructure.Identity;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Interceptors;
using AuthService.Infrastructure.Persistence.Repositories;
using AuthService.Shared.Common;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace AuthService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructuresDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPostgreSQL()
            .AddIdentity()
            .AddRepositories()
            .AddLogging(configuration);

        return services;
    }

    private static IServiceCollection AddPostgreSQL(this IServiceCollection services)
    {
        var connectionStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfiguration>>().Value.ConnectionStrings;
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<AuthDatabaseContext>((provider, options) =>
            options
                .AddInterceptors(provider.GetRequiredService<ISaveChangesInterceptor>())
                .UseNpgsql(connectionStrings.PostgreSQL).AddAsyncSeeding(provider));
        services.AddScoped<IAuthDatabaseContext>(provider => provider.GetRequiredService<AuthDatabaseContext>());
        services.AddScoped<AuthDatabaseContextInitialiser>();

        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AuthDatabaseContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(ISpecification<>), typeof(BaseSpecification<>));

        return services;
    }

    private static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((s, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(s)
            .Enrich.FromLogContext()
            .WriteTo.Console(new ExpressionTemplate(
                // Include trace and span ids when present.
                "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}",
                theme: TemplateTheme.Code)));

        return services;
    }
}
