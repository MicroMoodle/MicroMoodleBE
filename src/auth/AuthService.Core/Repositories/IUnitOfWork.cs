using AuthService.Core.Common;

namespace AuthService.DataAccess.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepository<T> Repository<T>() where T : BaseEntity;
}
