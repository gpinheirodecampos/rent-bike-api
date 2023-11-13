using Microsoft.AspNetCore.JsonPatch;
using RentAPI.Context;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public class BikeRepository : Repository<Bike>, IBikeRepository
    {
        public BikeRepository(AppDbContext contexto) : base(contexto)
        { 
        }

        public async IEnumerable<Bike> GetBikeByAvailability()
        {
            return Get().Where(b => b.Available == true).ToList();
        }

        public void UpdateBikeAvailability(int id)
        {
            var bike = GetById(b => b.BikeId == id);

            

            bike.Available = false;

            _context.Attach(bike);
            _context.Entry(bike).Property(b => b.Available).IsModified = true;
            _context.SaveChanges();
        }
    }
}
