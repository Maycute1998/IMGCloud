using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IMGCloud.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        T GetId(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task Remove(T entity);
        Task Delete(T entity);

        #region [Execute-Store-Procedure]
        //ISQLHelpers SQLHelper();
        #endregion
    }
}
