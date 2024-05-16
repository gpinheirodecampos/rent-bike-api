using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class ImageControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IImageService _imageService;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static ImageControllerTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "RentAPIDbTest")
                .Options;

            DatabaseInitializer.Initialize(dbContextOptions);
        }

        public ImageControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            _UnitOfWork = new UnitOfWork(new AppDbContext(dbContextOptions));
            _imageService = new ImageService(_UnitOfWork, _mapper);
        }

        [Fact]
        public async Task ImageController_GetAllImages_ReturnsAllImages()
        {
            // Arrange
            var controller = new ImageController(_imageService);

            // Act
            var result = await controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<List<ImageDTO>>();
            result.Result.As<OkObjectResult>().Value.As<List<ImageDTO>>().Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task ImageController_GetImageById_ReturnsImage()
        {
            // Arrange
            var controller = new ImageController(_imageService);
            var image = await _UnitOfWork.ImageRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await controller.GetById(image.ImageId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<ImageDTO>();
            result.Result.As<OkObjectResult>().Value.As<ImageDTO>().ImageId.Should().Be(image.ImageId);
        }

        [Fact]
        public async Task ImageController_AddImage_ReturnsOk()
        {
            // Arrange
            var controller = new ImageController(_imageService);
            var image = new ImageDTO
            {
                ImageId = Guid.NewGuid(),
                BikeId = await _UnitOfWork.BikeRepository.Get().Select(x => x.BikeId).FirstOrDefaultAsync(),
                Url = "https://www.bing.com"
            };

            // Act
            var result = await controller.Post(image);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_AddImage_ReturnsException()
        {
            // Arrange
            var controller = new ImageController(_imageService);
            var image = new ImageDTO
            {
                ImageId = Guid.NewGuid(),
                BikeId = Guid.NewGuid(),
                Url = "https://www.bing.com"
            };

            // Act
            Func<Task> result = async () => await controller.Post(image);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task ImageController_UpdateImage_ReturnsOk()
        {
            // Arrange
            var controller = new ImageController(_imageService);
            var image = await _UnitOfWork.ImageRepository.Get().FirstOrDefaultAsync();
            image!.Url = "https://www.chatgpt.com";
            var imageDTO = _mapper.Map<ImageDTO>(image);

            // Act
            var result = await controller.Put(image.ImageId, imageDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ImageController_UpdateImage_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ImageController(_imageService);
            var image = await _UnitOfWork.ImageRepository.Get().FirstOrDefaultAsync();
            image!.Url = "https://www.chatgpt.com";
            var imageDTO = _mapper.Map<ImageDTO>(image);

            // Act
            var result = await controller.Put(Guid.NewGuid(), imageDTO);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ImageController_DeleteImage_ReturnsOk()
        {
            // Arrange
            var controller = new ImageController(_imageService);
            var image = await _UnitOfWork.ImageRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await controller.Delete(image!.ImageId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task ImageController_DeleteImage_ReturnsException()
        {
            // Arrange
            var controller = new ImageController(_imageService);

            // Act
            Func<Task> result = async () => await controller.Delete(Guid.NewGuid());

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }
    }
}
