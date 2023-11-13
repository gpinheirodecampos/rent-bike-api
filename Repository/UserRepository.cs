using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;
using System.Linq.Expressions;

namespace RentAPI.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<User> GetUserByEmail(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }
    }
}
