using Rents.Domain.Entities;

namespace Rents.Infrastructure.Repository.Interfaces
{
    public interface IBikeRepository : IRepository<Bike>
    {
        IEnumerable<Bike> GetBikeByAvailability();

        Task UpdateBikeAvailability(Guid id);
    }
}
