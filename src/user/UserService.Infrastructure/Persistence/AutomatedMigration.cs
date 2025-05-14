using UserService.DataAccess.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.Persistence;

public static class AutomatedMigration
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<UserDatabaseContext>();

        if (context.Database.IsNpgsql()) await context.Database.MigrateAsync();
    }
}
