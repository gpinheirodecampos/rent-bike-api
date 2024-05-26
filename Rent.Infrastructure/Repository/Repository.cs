using Microsoft.EntityFrameworkCore;
using Rents.Infrastructure.Context;
using Rents.Infrastructure.Repository.Interfaces;
using System.Linq.Expressions;

namespace Rents.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext contexto)
        {
            _context = contexto;
        }

        /// <summary>
        /// Função que retorna todos os registros de uma entidade. Usar o include para incluir os relacionamentos.
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<T> Get(Expression<Func<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (include != null)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<T> GetByProperty(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking().Where(predicate);

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync();
        }

        virtual public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context?.Set<T>().Remove(entity);
        }


        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
