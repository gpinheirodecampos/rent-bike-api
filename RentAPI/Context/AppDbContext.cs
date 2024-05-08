using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;

namespace RentAPI.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Bike>? Bikes { get; set; }

        public DbSet<Image>? Images { get; set; }

        public DbSet<Rent>? Rents { get; set; }

        public new DbSet<User>? Users { get; set; }
    }
}
