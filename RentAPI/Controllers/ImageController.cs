using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;
using RentAPI.Services.Inferfaces;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> Get()
        {
            var images = await _imageService.Get();

            if (images.Count() == 0) { return NotFound("Nenhuma imagem encontrada."); }

            return Ok(images);
        }

        [HttpGet("{id:int}", Name = "ObterImage")]
        public async Task<ActionResult<ImageDTO>> GetById(Guid id)
        {
            var image = await _imageService.GetById(id);

            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ImageDTO imageDto)
        {
            if (imageDto is null) { return BadRequest(); }

            await _imageService.Add(imageDto);

            return Ok("Imagem registrada com sucesso!");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Guid id, ImageDTO imageDto)
        {
            if (id != imageDto.ImageId) { return BadRequest(); }

            await _imageService.Update(imageDto);

            return Ok(imageDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var image = await _imageService.GetById(id);
            if (image is null) { return NotFound(); }

            await _imageService.Delete(id);

            return Ok();
        }
    }
}
