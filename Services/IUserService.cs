using RentAPI.DTOs;

namespace RentAPI.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO>Get();
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<UserDTO> GetByEmailAsync(string email);
        Task AddAsync(UserDTO userDto);
        Task UpdateAsync(UserDTO userDto);
        Task DeleteAsync(Guid id);
    }
}
