using RentAPI.DTOs;
using RentAPI.Models;

namespace RentAPI.Services.Inferfaces
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
