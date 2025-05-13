using AuthService.API;
using AuthService.Application;
using AuthService.DataAccess;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Persistence;
using AuthService.Shared;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up!");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddSharedDependencies(builder.Configuration)
        .AddApplicationDependencies(builder.Configuration)
        .AddInfrastructuresDependencies(builder.Configuration)
        .AddAPIDependencies(builder.Configuration);

    await using var app = builder.Build();

    using var scope = app.Services.CreateScope();

    if (app.Environment.IsDevelopment())
    {
        await app.InitialiseDatabaseAsync();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthService");
            Log.Information("Swagger UI available at: /swagger/index.html");
        });
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.UseExceptionHandler();

    await app.RunAsync();

    Log.Information("Stopped cleanly");
    return 0;
} catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
