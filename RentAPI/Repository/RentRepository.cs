using RentAPI.Context;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;

namespace RentAPI.Repository
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        public RentRepository(AppDbContext contexto) : base(contexto)
        { 
        }
    }
}
