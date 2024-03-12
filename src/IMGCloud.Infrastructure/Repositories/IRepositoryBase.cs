using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Repositories;

public interface IRepositoryBase<TEntity, TKey> :
    IActionRepositoryBase<TEntity, TKey>,
    ISyncRepository<TEntity, TKey>,
    IQueryRepository<TEntity, TKey>,
    IDisposable
    where TEntity : EntityBase<TKey>
{
}

public interface IActionRepositoryBase<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<IList<TKey>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task EndTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

public interface ISyncRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    void Create(TEntity entity);
    void Create(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);
}

public interface IQueryRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    IQueryable<TEntity> FindAll(bool trackChanges = false);
    IQueryable<TEntity> FindAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includedProperties);
    IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);
    IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includedProperties, bool trackChanges = false);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TKey id, Expression<Func<TEntity, object>>[] includedProperties, CancellationToken cancellationToken = default);
}
