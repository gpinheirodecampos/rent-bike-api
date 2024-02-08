using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IUnityOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageController(IUnityOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetAsync()
        {
            var images = await _unitOfWork.ImageRepository.Get().ToListAsync();

            var imagesDto = _mapper.Map<List<ImageDTO>>(images);

            if (imagesDto is null) { return NotFound("Nao ha imagens cadastradas."); }

            return imagesDto;
        }

        [HttpGet("{id:int}", Name = "ObterImage")]
        public async Task<ActionResult<ImageDTO>> GetAsync(int id)
        {
            var image = await _unitOfWork.ImageRepository.GetByIdAsync(i => i.ImageId == id);

            var imageDto = _mapper.Map<ImageDTO>(image);

            if (imageDto is null) { return NotFound("Imagem nao encontrada."); }

            return imageDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ImageDTO imageDto)
        {
            if (imageDto is null) { return BadRequest(); }

            var image = _mapper.Map<Image>(imageDto);

            _unitOfWork.ImageRepository.Add(image);
            await _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterImage",
                new { id = image.BikeId }, image);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ImageDTO imageDto)
        {
            if (id != imageDto.BikeId) { return BadRequest(); }

            var image = _mapper.Map<Image>(imageDto);

            _unitOfWork.ImageRepository.Update(image);
            await _unitOfWork.Commit();

            return Ok(imageDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var image = await _unitOfWork.ImageRepository.GetByIdAsync(i => i.ImageId == id);

            if (image is null) { return NotFound("Image nao encontrada."); }

            _unitOfWork.ImageRepository.Delete(image);
            await _unitOfWork.Commit();

            return Ok();
        }
    }
}
