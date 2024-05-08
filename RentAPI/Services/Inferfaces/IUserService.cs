using RentAPI.DTOs;

namespace RentAPI.Services.Inferfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> Get();
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<UserDTO> AddAsync(UserDTO userDto);
        Task UpdateAsync(UserDTO userDto);
        Task DeleteAsync(Guid id);
    }
}
