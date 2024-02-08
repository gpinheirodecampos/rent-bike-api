using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public class BikeRepository : Repository<Bike>, IBikeRepository
    {
        public BikeRepository(AppDbContext contexto) : base(contexto)
        { 
        }

        public IQueryable<Bike> GetBikesImages()
        {
            return Get().Include(x => x.Images);
        }

        public IEnumerable<Bike> GetBikeByAvailability()
        {
            return Get().Where(b => b.Available).ToList();
        }

        public async Task UpdateBikeAvailability(int id)
        {
            var bike = await GetByIdAsync(b => b.BikeId == id);

            bike.Available = false;

            _context.Attach(bike);
            _context.Entry(bike).Property(b => b.Available).IsModified = true;
            _context.SaveChanges();
        }

    }
}
