using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace IMGCloud.Infrastructure.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    protected readonly ImgCloudContext dbContext;
    protected readonly IUnitOfWork unitOfWork;
    private bool disposed = false;

    protected RepositoryBase(ImgCloudContext dbContext, IUnitOfWork unitOfWork)
    {
        this.dbContext = dbContext;
        this.unitOfWork = unitOfWork;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    => this.dbContext.Database.BeginTransactionAsync(cancellationToken);

    public void Create(TEntity entity)
    => this.dbContext.Set<TEntity>().Add(entity);

    public void Create(IEnumerable<TEntity> entities)
    => this.dbContext.Set<TEntity>().AddRange(entities);

    public async Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await this.dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<IList<TKey>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await this.dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return entities.Select(n => n.Id).ToList();
    }

    public void Delete(TEntity entity)
    => this.dbContext.Set<TEntity>().Remove(entity);


    public void Delete(IEnumerable<TEntity> entities)
    => this.dbContext.RemoveRange(entities);

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.dbContext.Set<TEntity>().Remove(entity);
        return this.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        this.dbContext.Set<TEntity>().RemoveRange(entities);
        return this.SaveChangesAsync(cancellationToken);
    }

    public async Task EndTransactionAsync(CancellationToken cancellationToken = default)
    {
        await this.SaveChangesAsync(cancellationToken);
        await this.dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public IQueryable<TEntity> FindAll(bool trackChanges = false)
    => !trackChanges ? this.dbContext.Set<TEntity>().AsNoTracking()
                      : this.dbContext.Set<TEntity>();

    public IQueryable<TEntity> FindAll(bool trackChanges = false, params Expression<Func<TEntity, object>>[] includedProperties)
    {
        var items = FindAll(trackChanges);
        var results = includedProperties.Aggregate(items, (current, includedProperty) => current.Include(includedProperty));
        return results;
    }

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, bool trackChanges = false)
    => !trackChanges ? this.dbContext.Set<TEntity>().Where(expression).AsNoTracking()
                     : this.dbContext.Set<TEntity>().Where(expression);

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includedProperties, bool trackChanges = false)
    {
        var items = FindBy(expression, trackChanges);
        var results = includedProperties.Aggregate(items, (current, includedProperty) => current.Include(includedProperty));
        return results;
    }

    public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    => FindBy(x => x.Id!.Equals(id)).FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity?> GetByIdAsync(TKey id, Expression<Func<TEntity, object>>[] includedProperties, CancellationToken cancellationToken = default)
    => FindBy(x => x.Id!.Equals(id), includedProperties: includedProperties, trackChanges: false).FirstOrDefaultAsync(cancellationToken);

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    => this.dbContext.Database.RollbackTransactionAsync(cancellationToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    => this.unitOfWork.CommitAsync(cancellationToken);

    public void Update(TEntity entity)
    {
        if (this.dbContext.Entry(entity).State == EntityState.Unchanged)
        {
            return;
        }
        var existed = this.dbContext.Set<TEntity>().Find(entity.Id);

        ArgumentNullException.ThrowIfNull(existed);
        this.dbContext.Entry(existed).CurrentValues.SetValues(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    => entities.ToList().ForEach(e => this.Update(e));

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (this.dbContext.Entry(entity).State == EntityState.Unchanged)
        {
            return Task.CompletedTask;
        }

        var existed = this.dbContext.Set<TEntity>().Find(entity.Id, cancellationToken);
        ArgumentNullException.ThrowIfNull(existed);
        this.dbContext.Entry(existed).CurrentValues.SetValues(entity);
        return this.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        this.dbContext.UpdateRange(entities, cancellationToken);
        return SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RepositoryBase() // Noncompliant - Modify UnitOfWork.~UnitOfWork() so that it calls Dispose(false) and then returns.
    {
        // Cleanup
    }

    // Dispose(bool disposing) executes in two distinct scenarios.
    // If disposing equals true, the method has been called directly
    // or indirectly by a user's code. Managed and unmanaged resources
    // can be disposed.
    // If disposing equals false, the method has been called by the
    // runtime from inside the finalizer and you should not reference
    // other objects. Only unmanaged resources can be disposed.
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (!this.disposed)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                // Dispose managed resources.
                this.dbContext.Dispose();
                this.unitOfWork.Dispose();
            }

            // Note disposing has been done.
            disposed = true;

        }
    }
}

