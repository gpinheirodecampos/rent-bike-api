using RentAPI.Models;
using System.Linq.Expressions;

namespace RentAPI.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(Expression<Func<User, bool>> predicate);
    }
}
