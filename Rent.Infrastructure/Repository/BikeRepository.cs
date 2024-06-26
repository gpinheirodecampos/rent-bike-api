﻿using Microsoft.EntityFrameworkCore;
using Rents.Infrastructure.Context;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;

namespace Rents.Infrastructure.Repository
{
    public class BikeRepository : Repository<Bike>, IBikeRepository
    {
        public BikeRepository(AppDbContext contexto) : base(contexto)
        { 
        }

        public IEnumerable<Bike> GetBikeByAvailability()
        {
            return Get().Where(b => b.Available).ToList();
        }

        public async Task UpdateBikeAvailability(Guid id)
        {
            var bike = await GetByProperty(b => b.BikeId == id);

            bike.Available = false;

            _context.Attach(bike);
            _context.Entry(bike).Property(b => b.Available).IsModified = true;
            _context.SaveChanges();
        }

        public override void Add(Bike entity)
        {
            entity.BikeId = Guid.NewGuid();

            base.Add(entity);
        }
    }
}
