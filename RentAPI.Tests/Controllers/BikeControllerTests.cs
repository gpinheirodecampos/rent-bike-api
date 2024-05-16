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
    public class BikeControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IBikeService _bikeService;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static BikeControllerTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "RentAPIDbTest")
                .Options;

            DatabaseInitializer.Initialize(dbContextOptions);
        }

        public BikeControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            _UnitOfWork = new UnitOfWork(new AppDbContext(dbContextOptions));
            _bikeService = new BikeService(_UnitOfWork, _mapper);
        }

        [Fact]
        public async Task BikeController_GetAllBikes_ReturnsAllBikes()
        {
            // Arrange
            var controller = new BikeController(_bikeService);

            // Act
            var result = await controller.Get();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeOfType<List<BikeDTO>>();
            result.Result.As<OkObjectResult>().Value.As<List<BikeDTO>>().Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task BikeController_GetBikeById_ReturnsBike()
        {
            // Arrange
            var controller = new BikeController(_bikeService);
            var bike = await _UnitOfWork.BikeRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await controller.GetById(bike.BikeId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_PostBike_ReturnsOk()
        {
            // Arrange
            var controller = new BikeController(_bikeService);
            var bike = new BikeDTO
            {
                BikeId = Guid.NewGuid(),
                Available = true,
                Description = "Description",
                Name = "Name",
                TypeBike = Enums.Enum.TypeBike.New
            };

            // Act
            var result = await controller.Post(bike);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_AddBike_ReturnsException()
        {
            // Arrange
            var controller = new BikeController(_bikeService);
            var bike = await _UnitOfWork.BikeRepository.Get().FirstOrDefaultAsync();
            var bikeDto = _mapper.Map<BikeDTO>(bike);

            // Act
            Func<Task> result = async () => await controller.Post(bikeDto);

            // Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task BikeController_UpdateBike_ReturnsOk()
        {
            // Arrange
            var controller = new BikeController(_bikeService);
            var bike = await _UnitOfWork.BikeRepository.Get().FirstOrDefaultAsync();
            bike.TypeBike = Enums.Enum.TypeBike.Used;
            var bikeDto = _mapper.Map<BikeDTO>(bike);

            // Act
            var result = await controller.Put(bike.BikeId, bikeDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_UpdateBike_ReturnsBadRequest()
        {
            // Arrange
            var controller = new BikeController(_bikeService);
            var bike = await _UnitOfWork.BikeRepository.Get().FirstOrDefaultAsync();
            var bikeDto = _mapper.Map<BikeDTO>(bike);

            // Act
            var result = await controller.Put(Guid.NewGuid(), bikeDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task BikeController_DeleteBike_ReturnsOk()
        {
            // Arrange
            var controller = new BikeController(_bikeService);
            var bike = await _UnitOfWork.BikeRepository.Get().FirstOrDefaultAsync();

            // Act
            var result = await controller.Delete(bike.BikeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BikeController_DeleteBike_ReturnsNotFound()
        {
            // Arrange
            var controller = new BikeController(_bikeService);

            // Act
            var result = await controller.Delete(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
