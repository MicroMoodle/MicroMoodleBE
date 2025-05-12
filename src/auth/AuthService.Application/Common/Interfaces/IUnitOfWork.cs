using AuthService.Core.Common;

namespace AuthService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task RollBackChangesAsync(CancellationToken cancellationToken);
    IRepository<T> Repository<T>() where T : BaseEntity;
}
