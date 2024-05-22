using RentAPI.DTOs;

namespace RentAPI.Services.Inferfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> Get();
        Task<UserDTO> GetById(Guid id);
        Task<UserDTO> GetByEmail(string email);
        Task<UserDTO> Add(UserDTO userDto);
        Task Update(UserDTO userDto);
        Task Delete(Guid id);
    }
}
