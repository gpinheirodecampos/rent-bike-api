using Rents.Application.DTOs;
using Rents.Domain.Entities;

namespace Rents.Application.Services.Inferfaces
{
    public interface IBikeService
    {
        Task<IEnumerable<BikeDTO>> Get();
        Task<BikeDTO> GetById(Guid id);
        Task<BikeDTO> Add(BikeDTO bikeDto);
        Task Update(BikeDTO bikeDto);
        Task Delete(BikeDTO bikeDto);
    }
}
