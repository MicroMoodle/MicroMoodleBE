using AuthService.Application.MappingProfiles;
using AuthService.Application.Models.Validators;
using AuthService.Application.Services.TodoItem;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace AuthService.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLibraries();

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
