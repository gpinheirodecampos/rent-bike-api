using System.Linq.Expressions;

namespace RentAPI.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(Expression<Func<T, object>>? include = null);

        Task<T> GetByProperty(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
