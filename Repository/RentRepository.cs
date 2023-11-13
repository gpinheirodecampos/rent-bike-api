using RentAPI.Context;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        public RentRepository(AppDbContext contexto) : base(contexto)
        { 
        }
    }
}
