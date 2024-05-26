using Microsoft.EntityFrameworkCore;
using Rents.Infrastructure.Context;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;
using System.Linq.Expressions;

namespace Rents.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
