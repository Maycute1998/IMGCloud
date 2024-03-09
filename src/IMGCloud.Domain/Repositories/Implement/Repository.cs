using IMGCloud.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IMGCloud.Domain.Repositories.Implement
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(null, nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            return _dbContext.AddRangeAsync(entities);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet!.ToListAsync();
        }

        public T GetById(int id)
        {
            var result = _dbSet.Find(id);
            ArgumentNullException.ThrowIfNull(result);

            return result;
        }

        public T? GetById(Expression<Func<T, bool>> predicate)
        {
            return (predicate is not null) ? _dbSet.Where(predicate).FirstOrDefault() : _dbSet.FirstOrDefault();
        }

        public Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate = null!,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null!,
           bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include is not null) query = include(query);

            if (predicate is not null) query = query.Where(predicate);

            if (orderBy is not null)
            {
                return orderBy(query).FirstOrDefaultAsync();
            }

            return query.FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
