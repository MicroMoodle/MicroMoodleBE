using AuthService.API.Filters;
using AuthService.Shared.Common;
using Microsoft.OpenApi.Models;

namespace AuthService.API;

public static class APIDependencyInjection
{
    public static IServiceCollection AddAPIDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();

        services.AddControllers();

        services.AddSwagger();

        return services;
    }

    private static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                        .AllowCredentials();
                });
        });

        return services;
    }

    private static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            options.Filters.Add<ValidateModelAttribute>();
        });

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
