using System.Reflection;
using AuthService.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace AuthService.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddLibraries()
            .AddMediatR();

        return services;
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
