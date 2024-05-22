using RentAPI.DTOs;

namespace RentAPI.Services.Inferfaces
{
    public interface IRentService
    {
        Task<IEnumerable<RentDTO>> Get();
        Task<RentDTO> GetById(Guid id);
        Task<RentDTO> GetByUserEmail(string email);
        Task<RentDTO> GetByBikeId(Guid id);
        Task Add(RentDTO rentDto);
        Task Update(RentDTO rentDto);
        Task Delete(Guid id);
    }
}
