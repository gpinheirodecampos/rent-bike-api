using Microsoft.AspNetCore.JsonPatch;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public interface IBikeRepository : IRepository<Bike>
    {
        IEnumerable<Bike> GetBikeByAvailability();

        void UpdateBikeAvailability(int id);
    }
}
