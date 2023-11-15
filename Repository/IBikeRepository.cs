using Microsoft.AspNetCore.JsonPatch;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public interface IBikeRepository : IRepository<Bike>
    {
        IEnumerable<Bike> GetBikeByAvailability();

        Task UpdateBikeAvailability(int id);
    }
}
