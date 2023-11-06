using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Filters;
using RentAPI.Models;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<User>>> GetAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            if (users is null) { return NotFound("Nao ha usuarios cadastrados."); }

            return users;
        }

        [HttpGet("{id:int}", Name = "ObterUser")]
        public async Task<ActionResult<User>> GetAsync(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null) { return NotFound("Usuario nao encontrado."); }

            return user;
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            if (user is null) { return BadRequest(); }

            _context.Users.Add(user);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterUser", 
                new { id = user.UserId }, user);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, User user)
        {
            if (id != user.UserId) { return BadRequest(); }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user is null) { return NotFound("Usuario nao encontrado."); }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}
