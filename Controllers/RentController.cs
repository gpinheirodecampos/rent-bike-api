using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;
using System.Formats.Asn1;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetAsync()
        {
            var rents = await _context.Rents.AsNoTracking().ToListAsync();

            if (rents is null) { return NotFound("Nao ha rents cadastradas."); }

            return rents;
        }

        [HttpGet("{id:int}", Name = "ObterRent")]
        public async Task<ActionResult<Rent>> GetAsync(int id)
        {
            var Rent = await _context.Rents.AsNoTracking().FirstOrDefaultAsync(u => u.RentId == id);

            if (Rent is null) { return NotFound("Rent nao encontrada."); }

            return Rent;
        }

        [HttpPost]
        public ActionResult Post(Rent Rent)
        {
            if (Rent is null) { return BadRequest(); }

            _context.Rents.Add(Rent);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterRent",
                new { id = Rent.RentId }, Rent);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Rent rent)
        {
            if (id != rent.RentId) { return BadRequest(); }

            _context.Entry(rent).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(rent);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var Rent = _context.Rents.FirstOrDefault(u => u.RentId == id);

            if (Rent is null) { return NotFound("Rent nao encontrada."); }

            _context.Rents.Remove(Rent);
            _context.SaveChanges();

            return Ok();
        }
    }
}
