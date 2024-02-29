using Microsoft.AspNetCore.JsonPatch;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public interface IBikeRepository : IRepository<Bike>
    {
        IQueryable<Bike> GetBikesImages();

        IEnumerable<Bike> GetBikeByAvailability();

        Task UpdateBikeAvailability(Guid id);
    }
}
