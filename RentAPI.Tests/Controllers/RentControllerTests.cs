using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Controllers;
using RentAPI.DTOs;
using RentAPI.DTOs.Mappings;
using RentAPI.Models;
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
    public class RentControllerTests : ControllerTestsBase<RentController, RentService>
    {
        [Fact]
        public async Task RentController_GetAllRents_ReturnsAllRents()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RentController_GetRentById_ReturnsRent()
        {
            // Arrange
            var rent = await _unitOfWork.RentRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetById(rent.RentId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<RentDTO>();
        }

        [Fact]
        public async Task RentController_GetRentByUserEmail_ReturnsOk()
        {
            // Arrange
            var user = await _unitOfWork.UserRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetByUserEmail(user.UserEmail);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        // get rent by bike id
        public async Task RentController_GetRentByBikeId_ReturnsRent()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetByBikeId(bike.BikeId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RentController_AddRent_ReturnsOk()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().LastOrDefaultAsync();
            var user = await _unitOfWork.UserRepository.Get().LastOrDefaultAsync();
            var rent = new RentDTO
            {
                RentId = Guid.NewGuid(),
                UserId = user.UserId,
                BikeId = bike.BikeId,
            };

            // Act
            var result = await _controller.Post(rent);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RentController_AddRent_ReturnsBadRequest()
        {
            // Arrange
            var rent = new RentDTO();

            // Act
            var result = await _controller.Post(rent);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task RentController_AddRent_ReturnsBicicletaNaoEncontradaException()
        {
            // Arrange
            var rent = new RentDTO
            {
                RentId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BikeId = Guid.NewGuid(),
            };

            // Act
            Func<Task> result = async () => await _controller.Post(rent);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task RentController_AddRent_ReturnsBicicletaJaAlugadaException()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().LastOrDefaultAsync();
            bike.Available = false;
            _unitOfWork.BikeRepository.Update(bike);
            await _unitOfWork.Commit();
            var user = await _unitOfWork.UserRepository.Get().LastOrDefaultAsync();
            var rent = new RentDTO
            {
                RentId = Guid.NewGuid(),
                UserId = user.UserId,
                BikeId = bike.BikeId,
            };

            // Act
            Func<Task> result = async () => await _controller.Post(rent);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task RentController_UpdateRent_ReturnsOk()
        {
            // Arrange
            var rent = await _unitOfWork.RentRepository.Get().FirstOrDefaultAsync();
            rent.DateEnd = DateTime.Now;
            var rentDto = _mapper.Map<RentDTO>(rent);

            // Act
            var result = await _controller.Put(rent.RentId, rentDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RentController_DeleteRent_ReturnsOk()
        {
            // Arrange
            var rent = await _unitOfWork.RentRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.Delete(rent.RentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        protected override RentService CreateServiceInstance()
        {
            // Crie uma instância específica de ExampleService com quaisquer parâmetros adicionais necessários
            return new RentService(_unitOfWork, _mapper);
        }
    }
}
