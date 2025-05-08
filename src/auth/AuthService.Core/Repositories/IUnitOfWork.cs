using AuthService.Core.Common;

namespace AuthService.Core.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IRepository<T> Repository<T>() where T : BaseEntity;
}
