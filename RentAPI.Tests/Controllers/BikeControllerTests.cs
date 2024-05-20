using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Controllers;
using RentAPI.DTOs;
using RentAPI.Services;

namespace RentAPI.Tests.Controllers
{
    public class BikeControllerTests : ControllerTestsBase<BikeController, BikeService>
    {
        [Fact]
        public async Task BikeController_GetAllBikes_ReturnsAllBikes()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_GetBikeById_ReturnsBike()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetById(bike.BikeId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_PostBike_ReturnsOk()
        {
            // Arrange
            var bike = new BikeDTO
            {
                BikeId = Guid.NewGuid(),
                Available = true,
                Description = "Description",
                Name = "Name",
                TypeBike = Enums.Enum.TypeBike.New
            };

            // Act
            var result = await _controller.Post(bike);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_AddBike_ReturnsException()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().FirstOrDefaultAsync();
            var bikeDto = _mapper.Map<BikeDTO>(bike);

            // Act
            Func<Task> result = async () => await _controller.Post(bikeDto);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task BikeController_UpdateBike_ReturnsOk()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().FirstOrDefaultAsync();
            bike.TypeBike = Enums.Enum.TypeBike.Used;
            var bikeDto = _mapper.Map<BikeDTO>(bike);

            // Act
            var result = await _controller.Put(bike.BikeId, bikeDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_UpdateBike_ReturnsBadRequest()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().FirstOrDefaultAsync();
            var bikeDto = _mapper.Map<BikeDTO>(bike);

            // Act
            var result = await _controller.Put(Guid.NewGuid(), bikeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task BikeController_DeleteBike_ReturnsOk()
        {
            // Arrange
            var bike = await _unitOfWork.BikeRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.Delete(bike.BikeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_DeleteBike_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        protected override BikeService CreateServiceInstance()
        {
            // Crie uma instância específica de ExampleService com quaisquer parâmetros adicionais necessários
            return new BikeService(_unitOfWork, _mapper);
        }
    }
}
