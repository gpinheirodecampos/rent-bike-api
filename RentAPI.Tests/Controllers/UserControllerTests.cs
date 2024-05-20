using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Controllers;
using RentAPI.DTOs;
using RentAPI.Services;

namespace RentAPI.Tests.Controllers
{
    public class UserControllerTests : ControllerTestsBase<UserController, UserService>
    {
        [Fact]
        public async Task UserController_GetAllUsers_ReturnsAllUsers()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_GetUserById_ReturnsUser()
        {
            // Arrange
            var user = await _unitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetById(user.UserId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_GetUserByEmail_ReturnsUser()
        {
            // Arrange
            var user = await _unitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetByEmail(user.UserEmail);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_AddUser_ReturnsOk()
        {
            // Arrange
            var user = new UserDTO
            {
                UserId = Guid.NewGuid(),
                UserName = "gabriel",
                UserEmail = "gabriel@mail.com",
                Password = "123"
            };

            // Act
            var result = await _controller.Post(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_AddUser_ReturnsException()
        {
            // Arrange
            var user = new UserDTO
            {
                UserId = Guid.NewGuid(),
                UserEmail = "gabs@mail.com",
                UserName = "gabriel",
                Password = "123"
            };

            // Act
            Func<Task> result = async () => await _controller.Post(user);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task UserController_UpdateUser_ReturnsOk()
        {
            // Arrange
            var user = await _unitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();
            user.UserName = "gabriel123";
            var userDto = _mapper.Map<UserDTO>(user);

            // Act
            var result = await _controller.Put(userDto.UserId, userDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_UpdateUser_ReturnsBadRequest()
        {
            // Arrange
            var user = await _unitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();
            user.UserName = "gabriel123";
            var userDto = _mapper.Map<UserDTO>(user);

            // Act
            var result = await _controller.Put(Guid.NewGuid(), userDto);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UserController_DeleteUser_ReturnsOk()
        {
            // Arrange
            var user = await _unitOfWork.UserRepository.Get(x => x.Rent).FirstOrDefaultAsync();

            // Act
            var result = await _controller.Delete(user.UserId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        protected override UserService CreateServiceInstance()
        {
            // Crie uma instância específica de ExampleService com quaisquer parâmetros adicionais necessários
            return new UserService(_unitOfWork, _mapper);
        }
    }
}
