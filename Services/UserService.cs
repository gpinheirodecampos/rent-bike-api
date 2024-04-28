using AutoMapper;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository;
using System;

namespace RentAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;

        public UserService(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> Get()
        {
            var users = _unityOfWork.UserRepository.Get();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _unityOfWork.UserRepository.GetByIdAsync(x => x.UserId == id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _unityOfWork.UserRepository.GetUserByEmail(x => x.UserEmail == email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task AddAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unityOfWork.UserRepository.Add(user);
            await _unityOfWork.Commit();
        }

        public async Task UpdateAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unityOfWork.UserRepository.Update(user);
            await _unityOfWork.Commit();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _unityOfWork.UserRepository.GetByIdAsync(x => x.UserId == id);
            _unityOfWork.UserRepository.Delete(user);
            await _unityOfWork.Commit();
        }
    }
}
