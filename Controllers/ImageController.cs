using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAsync()
        {
            var images = await _context.Images.AsNoTracking().ToListAsync();

            if (images is null) { return NotFound("Nao ha imagens cadastradas."); }

            return images;
        }

        [HttpGet("{id:int}", Name = "ObterImage")]
        public async Task<ActionResult<Image>> GetAsync(int id)
        {
            var image = await _context.Images.AsNoTracking().FirstOrDefaultAsync(u => u.ImageId == id);

            if (image is null) { return NotFound("Imagem nao encontrada."); }

            return image;
        }

        [HttpPost]
        public ActionResult Post(Image image)
        {
            if (image is null) { return BadRequest(); }

            _context.Images.Add(image);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterImage",
                new { id = image.BikeId }, image);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Image image)
        {
            if (id != image.BikeId) { return BadRequest(); }

            _context.Entry(image).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(image);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var image = _context.Images.FirstOrDefault(u => u.ImageId == id);

            if (image is null) { return NotFound("Image nao encontrada."); }

            _context.Images.Remove(image);
            _context.SaveChanges();

            return Ok();
        }
    }
}
