using AuthService.Core.Common;
using AuthService.Core.Repositories;
using AuthService.DataAccess.Persistence;

namespace AuthService.Infrastructure.Repositories;

public class UnitOfWork(AuthDatabaseContext dbContext) : IUnitOfWork
{
    private readonly IDictionary<Type, dynamic> _repositories = new Dictionary<Type, dynamic>();

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        var entityType = typeof(T);

        if (_repositories.ContainsKey(entityType))
        {
            return _repositories[entityType];
        }

        var repositoryType = typeof(BaseRepository<>);

        var repository =
            Activator.CreateInstance(repositoryType.MakeGenericType(entityType), dbContext);

        if (repository == null)
        {
            throw new NullReferenceException("Repository should not be null");
        }

        _repositories.Add(entityType, repository);

        return (IRepository<T>) repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public async Task RollBackChangesAsync()
    {
        await dbContext.Database.RollbackTransactionAsync();
    }
}
