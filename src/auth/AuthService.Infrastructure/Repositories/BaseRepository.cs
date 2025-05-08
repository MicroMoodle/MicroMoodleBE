using AuthService.Core.Common;
using AuthService.Core.Exceptions;
using AuthService.Core.Repositories;
using AuthService.DataAccess.Persistence;
using AuthService.DataAccess.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories;

public class Repository<TEntity>(AuthDatabaseContext context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public async Task<List<TEntity>> GetAllAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<TEntity> GetFirstOrThrowAsync(ISpecification<TEntity> spec)
    {
        var entity = await ApplySpecification(spec).FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<TEntity?> GetFirstAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        await context.SaveChangesAsync();

        return addedEntity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = DbSet.Remove(entity).Entity;
        await context.SaveChangesAsync();

        return removedEntity;
    }

    public TEntity Attach(TEntity entity)
    {
        return DbSet.Attach(entity).Entity;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>().AsQueryable(), spec);
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await context.AddRangeAsync(entities);
        await context.SaveChangesAsync();

        return entities;
    }
}
