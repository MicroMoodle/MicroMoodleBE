using AuthService.API.Filters;
using AuthService.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace AuthService.API;

public static class APIDependencyInjection
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCors()
            .AddControllers()
            .AddSwagger()
            .AddGlobalExceptionHandler();

        return services;
    }

    private static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
        });

        return services;
    }

    private static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            options.Filters.Add<ResponseWrapperFilter>();
        });

        return services;
    }

    private static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    // Change the auth scheme later
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.AddSecurityDefinition("Cookie",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Name = ".AspNetCore.Cookies",
                    Description = "Cookie-based authentication. Provide the authentication cookie."
                });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Cookie" }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}
