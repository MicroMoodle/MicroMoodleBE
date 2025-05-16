using AuthService.Core.Common;

namespace AuthService.Application.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetFirstOrThrowAsync(ISpecification<TEntity> spec);

    Task<TEntity?> GetFirstAsync(ISpecification<TEntity> spec);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> Find(ISpecification<TEntity> spec);

    Task<int> CountAsync(ISpecification<TEntity> spec);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);

    TEntity Attach(TEntity entity);

    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
}
