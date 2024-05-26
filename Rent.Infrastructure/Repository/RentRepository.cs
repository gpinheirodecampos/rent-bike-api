using Rents.Infrastructure.Context;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;

namespace Rents.Infrastructure.Repository
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        public RentRepository(AppDbContext contexto) : base(contexto)
        { 
        }
    }
}
