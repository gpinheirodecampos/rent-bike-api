using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Controllers;
using RentAPI.DTOs;
using RentAPI.Services;

namespace RentAPI.Tests.Controllers
{
    public class ImageControllerTests : ControllerTestsBase<ImageController, ImageService>
    {
        [Fact]
        public async Task ImageController_GetAllImages_ReturnsAllImages()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_GetImageById_ReturnsImage()
        {
            // Arrange
            var image = await _unitOfWork.ImageRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.GetById(image.ImageId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_AddImage_ReturnsOk()
        {
            // Arrange
            var image = new ImageDTO
            {
                ImageId = Guid.NewGuid(),
                BikeId = await _unitOfWork.BikeRepository.Get().Select(x => x.BikeId).FirstOrDefaultAsync(),
                Url = "https://www.bing.com"
            };

            // Act
            var result = await _controller.Post(image);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_AddImage_ReturnsException()
        {
            // Arrange
            var imageAdded = await _unitOfWork.ImageRepository.Get().FirstOrDefaultAsync();
            var image = new ImageDTO
            {
                ImageId = Guid.NewGuid(),
                BikeId = Guid.NewGuid(),
                Url = imageAdded.Url
            };

            // Act
            Func<Task> result = async () => await _controller.Post(image);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task ImageController_UpdateImage_ReturnsOk()
        {
            // Arrange
            var image = await _unitOfWork.ImageRepository.Get().FirstOrDefaultAsync();
            image!.Url = "https://www.chatgpt.com";
            var imageDTO = _mapper.Map<ImageDTO>(image);

            // Act
            var result = await _controller.Put(image.ImageId, imageDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_UpdateImage_ReturnsBadRequest()
        {
            // Arrange
            var image = await _unitOfWork.ImageRepository.Get().FirstOrDefaultAsync();
            image!.Url = "https://www.chatgpt.com";
            var imageDTO = _mapper.Map<ImageDTO>(image);

            // Act
            var result = await _controller.Put(Guid.NewGuid(), imageDTO);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ImageController_DeleteImage_ReturnsOk()
        {
            // Arrange
            var image = await _unitOfWork.ImageRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await _controller.Delete(image!.ImageId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_DeleteImage_ReturnsException()
        {
            // Act
            Func<Task> result = async () => await _controller.Delete(Guid.NewGuid());

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        protected override ImageService CreateServiceInstance()
        {
            return new ImageService(_unitOfWork, _mapper);
        }
    }
}
