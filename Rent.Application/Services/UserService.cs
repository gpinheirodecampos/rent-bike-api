﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rents.Application.DTOs;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;
using Rents.Application.Services.Inferfaces;
using System;

namespace Rents.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;

        public UserService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            var users = await _UnitOfWork.UserRepository.Get(x => x.Rent).ToListAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetById(Guid id)
        {
            var user = await _UnitOfWork.UserRepository.GetByProperty(x => x.UserId == id, x => x.Rent);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _UnitOfWork.UserRepository.GetByProperty(x => x.UserEmail == email, x => x.Rent);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> Add(UserDTO userDto)
        {
            var userExists = await _UnitOfWork.UserRepository.GetByProperty(x => x.UserEmail == userDto.UserEmail);

            if (userExists != null) { throw new Exception("Usuário já cadastrado."); }

            var user = _mapper.Map<User>(userDto);

            _UnitOfWork.UserRepository.Add(user);

            await _UnitOfWork.Commit();

            return userDto;
        }

        public async Task Update(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            _UnitOfWork.UserRepository.Update(user);

            await _UnitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var user = await _UnitOfWork.UserRepository.GetByProperty(x => x.UserId == id);

            _UnitOfWork.UserRepository.Delete(user);

            await _UnitOfWork.Commit();
        }
    }
}
