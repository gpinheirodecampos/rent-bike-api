using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAPI.Tests.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize(DbContextOptions<AppDbContext> dbContextOptions)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    UserEmail = "gabs@mail.com",
                    UserName = "gabs",
                    Password = "123"
                };
                context.Users.Add(user);

                var bike = new Bike
                {
                    BikeId = Guid.NewGuid(),
                    Name = "Bike 1",
                    Description = "Bike 1",
                    Available = true
                };
                context.Bikes.Add(bike);

                var image = new Image
                {
                    ImageId = Guid.NewGuid(),
                    BikeId = bike.BikeId,
                    Url = "https://www.google.com"
                };
                context.Images.Add(image);

                var rent = new Rent
                {
                    RentId = Guid.NewGuid(),
                    DateEnd = DateTime.Now,
                    DateStart = DateTime.Now,
                    BikeId = bike.BikeId,
                    UserId = user.UserId
                };
                context.Rents.Add(rent);

                context.SaveChanges();
            }
        }
    }
}
