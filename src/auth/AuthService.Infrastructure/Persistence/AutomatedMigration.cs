using AuthService.DataAccess.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence;

public static class AutomatedMigration
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<AuthDatabaseContext>();

        if (context.Database.IsNpgsql()) await context.Database.MigrateAsync();
    }
}
