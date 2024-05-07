using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;
using RentAPI.Services.Inferfaces;
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

        public async Task<IEnumerable<UserDTO>> Get()
        {
            var users = await _unityOfWork.UserRepository.Get(x => x.Rent).ToListAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _unityOfWork.UserRepository.GetByIdAsync(x => x.UserId == id, x => x.Rent);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _unityOfWork.UserRepository.GetUserByEmail(x => x.UserEmail == email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AddAsync(UserDTO userDto)
        {
            var userExists = await _unityOfWork.UserRepository.GetUserByEmail(x => x.UserEmail == userDto.UserEmail);
            if (userExists != null) { throw new Exception("Usuário já cadastrado."); }

            var user = _mapper.Map<User>(userDto);
            _unityOfWork.UserRepository.Add(user);
            await _unityOfWork.Commit();

            return userDto;
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
