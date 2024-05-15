using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Controllers;
using RentAPI.DTOs;
using RentAPI.DTOs.Mappings;
using RentAPI.Repository;
using RentAPI.Repository.Interfaces;
using RentAPI.Services;
using RentAPI.Services.Inferfaces;
using RentAPI.Tests.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAPI.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUserService _userService;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static UserControllerTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "RentAPIDbTest")
                .Options;

            DatabaseInitializer.Initialize(dbContextOptions);
        }

        public UserControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            _UnitOfWork = new UnitOfWork(new AppDbContext(dbContextOptions));
            _userService = new UserService(_UnitOfWork, _mapper);
        }

        [Fact]
        public async Task UserController_GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var controller = new UserController(_userService);

            // Act
            var result = await controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<List<UserDTO>>();
            result.Result.As<OkObjectResult>().Value.As<List<UserDTO>>().Count.Should().BeGreaterThanOrEqualTo(1);
            result.Result.As<OkObjectResult>().Value.As<List<UserDTO>>().First().Rent.Count.Should().BeGreaterThanOrEqualTo(1);

        }

        [Fact]
        public async Task UserController_GetUserById_ReturnsUser()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = await _UnitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();

            // Act
            var result = await controller.GetById(user.UserId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<UserDTO>();
            result.Result.As<OkObjectResult>().Value.As<UserDTO>().UserId.Should().Be(user.UserId);
            result.Result.As<OkObjectResult>().Value.As<UserDTO>().Rent.Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task UserController_GetUserByEmail_ReturnsUser()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = await _UnitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();

            // Act
            var result = await controller.GetByEmail(user.UserEmail);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<UserDTO>();
            result.Result.As<OkObjectResult>().Value.As<UserDTO>().UserId.Should().Be(user.UserId);
            result.Result.As<OkObjectResult>().Value.As<UserDTO>().Rent.Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task UserController_AddUser_ReturnsOk()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = new UserDTO
            {
                UserId = Guid.NewGuid(),
                UserName = "gabriel",
                UserEmail = "gabriel@mail.com",
                Password = "123"
            };

            // Act
            var result = await controller.Post(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_AddUser_ReturnsException()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = new UserDTO
            {
                UserId = Guid.NewGuid(),
                UserEmail = "gabs@mail.com",
                UserName = "gabriel",
                Password = "123"
            };

            // Act
            Func<Task> result = async () => await controller.Post(user);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }


        [Fact]
        public async Task UserController_UpdateUser_ReturnsOk()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = await _UnitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();
            user.UserName = "gabriel123";
            var userDto = _mapper.Map<UserDTO>(user);

            // Act
            var result = await controller.Put(userDto.UserId, userDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_UpdateUser_ReturnsBadRequest()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = await _UnitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();
            user.UserName = "gabriel123";
            var userDto = _mapper.Map<UserDTO>(user);

            // Act
            var result = await controller.Put(Guid.NewGuid(), userDto);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UserController_DeleteUser_ReturnsOk()
        {
            // Arrange
            var controller = new UserController(_userService);
            var user = await _UnitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();

            // Act
            var result = await controller.Delete(user.UserId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
