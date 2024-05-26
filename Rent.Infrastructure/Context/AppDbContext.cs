using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rents.Domain.Entities;

namespace Rents.Infrastructure.Context
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
