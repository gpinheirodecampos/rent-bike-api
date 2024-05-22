using Microsoft.AspNetCore.JsonPatch;
using RentAPI.Models;

namespace RentAPI.Repository.Interfaces
{
    public interface IBikeRepository : IRepository<Bike>
    {
        IEnumerable<Bike> GetBikeByAvailability();

        Task UpdateBikeAvailability(Guid id);
    }
}
