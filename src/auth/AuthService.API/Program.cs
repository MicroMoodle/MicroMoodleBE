using AuthService.API;
using AuthService.Application;
using AuthService.DataAccess;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Persistence;
using AuthService.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedDependencies(builder.Configuration)
    .AddApplicationDependencies(builder.Configuration)
    .AddAPIDependencies(builder.Configuration)
    .AddInfrastructuresDependencies(builder.Configuration);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthService");
        logger.LogInformation("Swagger UI available at: /swagger/index.html");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
