using System.Reflection;
using AuthService.Application.Common.Interfaces;
using AuthService.Core.Common;
using AuthService.Core.Entities.Example;
using AuthService.Infrastructure.Identity;
using AuthService.Shared.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence;

public class AuthDatabaseContext(DbContextOptions<AuthDatabaseContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IAuthDatabaseContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
