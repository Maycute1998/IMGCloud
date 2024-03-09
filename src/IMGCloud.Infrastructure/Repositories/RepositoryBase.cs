using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace IMGCloud.Infrastructure.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Create(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public IList<TKey> Create(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TKey>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EndTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> FindAll(bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> FindAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includedProperties)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, bool trackChanges = false, params Expression<Func<TEntity, object>>[] includedProperties)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetByIdAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includedProperties)
    {
        throw new NotImplementedException();
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
