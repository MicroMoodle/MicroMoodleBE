using AuthService.Core.Entities.Example;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.Common.Interfaces;

public interface IAuthDatabaseContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
