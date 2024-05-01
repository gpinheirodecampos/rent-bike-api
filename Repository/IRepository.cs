using System.Linq.Expressions;

namespace RentAPI.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(Expression<Func<T, object>>? include = null);

        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
