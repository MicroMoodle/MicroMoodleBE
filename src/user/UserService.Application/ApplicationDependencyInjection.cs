using UserService.Application.Exceptions;
using UserService.Application.MappingProfiles;
using UserService.Application.Models.Validators;
using UserService.Application.Services.TodoItem;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace UserService.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddGlobalExceptionHandler()
            .AddServices()
            .AddLibraries();

        return services;
    }

    private static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoItemService, TodoItemService>();

        return services;
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(IValidatorMarker));

        services.AddAutoMapper(typeof(IMappingProfilesMarker));

        return services;
    }
}
