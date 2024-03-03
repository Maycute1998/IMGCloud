using IMGCloud.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IMGCloud.Domain.Repositories.Implement
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public Task AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet!.ToListAsync();
        }

        public T GetById(int id)
        {
            var result = _dbSet.Find(id);
            if (result == null)
            {
                throw new ArgumentNullException("Id null");
            }

            return result;
        }

        public T GetId(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null) query = query.Where(predicate);
            return query.FirstOrDefault();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
