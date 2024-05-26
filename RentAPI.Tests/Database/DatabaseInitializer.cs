using Microsoft.EntityFrameworkCore;
using Rents.Infrastructure.Context;
using Rents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAPI.Tests.Database
{
    public static class DatabaseInitializer
    {
        public static void Reinitialize(DbContextOptions<AppDbContext> dbContextOptions)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        public static void Initialize(DbContextOptions<AppDbContext> dbContextOptions)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var user1 = new User
                {
                    UserId = Guid.NewGuid(),
                    UserEmail = "gabs@mail.com",
                    UserName = "gabs",
                    Password = "123"
                };

                var user2 = new User
                {
                    UserId = Guid.NewGuid(),
                    UserEmail = "gabs2@mail.com",
                    UserName = "gabs2",
                    Password = "123"
                };

                context.Users.Add(user1);
                context.Users.Add(user2);

                var bike1 = new Bike
                {
                    BikeId = Guid.NewGuid(),
                    Name = "Bike 1",
                    Description = "Bike 1",
                    Available = true
                };

                var bike2 = new Bike
                {
                    BikeId = Guid.NewGuid(),
                    Name = "Bike 2",
                    Description = "Bike 2",
                    Available = true
                };

                context.Bikes.Add(bike1);
                context.Bikes.Add(bike2);

                var image1 = new Image
                {
                    ImageId = Guid.NewGuid(),
                    BikeId = bike1.BikeId,
                    Url = "https://www.google.com"
                };

                var image2 = new Image
                {
                    ImageId = Guid.NewGuid(),
                    BikeId = bike2.BikeId,
                    Url = "https://www.google2.com"
                };

                context.Images.Add(image1);
                context.Images.Add(image2);

                var rent = new Rent
                {
                    RentId = Guid.NewGuid(),
                    DateEnd = DateTime.Now,
                    DateStart = DateTime.Now,
                    BikeId = bike1.BikeId,
                    UserId = user1.UserId
                };
                context.Rents.Add(rent);

                context.SaveChanges();
            }
        }
    }
}
