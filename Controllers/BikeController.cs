using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BikeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetAsync()
        {
            var bikes = await _context.Bikes.AsNoTracking().ToListAsync();

            if (bikes is null) { return NotFound("Nao ha bikes cadastradas."); }

            return bikes;
        }

        [HttpGet("{id:int}", Name = "ObterBike")]
        public async Task<ActionResult<Bike>> GetAsync(int id)
        {
            var bike = await _context.Bikes.AsNoTracking().FirstOrDefaultAsync(u => u.BikeId == id);

            if (bike is null) { return NotFound("Bike nao encontrada."); }

            return bike;
        }

        [HttpGet("images")]
        public async Task<ActionResult<IEnumerable<Bike>>> GetBikesImagesAsync()
        {
            var bikes = await _context.Bikes.AsNoTracking().Include(i => i.Images).ToListAsync();

            if (bikes is null) { return NotFound("Nao ha bikes cadastradas."); }

            return bikes;
        }

        [HttpPost]
        public ActionResult Post(Bike bike)
        {
            if (bike is null) { return BadRequest(); }

            _context.Bikes.Add(bike);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterBike",
                new { id = bike.BikeId }, bike);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Bike bike)
        {
            if (id != bike.BikeId) { return BadRequest(); }

            _context.Entry(bike).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(bike);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var bike = _context.Bikes.FirstOrDefault(u => u.BikeId == id);

            if (bike is null) { return NotFound("Bike nao encontrada."); }

            _context.Bikes.Remove(bike);
            _context.SaveChanges();

            return Ok();
        }
    }
}
