using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IMGCloud.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        T GetById(int id);
        T GetId(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);

        void Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void Delete(T entity);

        #region [Execute-Store-Procedure]
        //ISQLHelpers SQLHelper();
        #endregion
    }
}
