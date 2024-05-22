using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // image/
        /// <summary>
        /// Obtém uma lista de imagens cadastradas.
        /// </summary>
        /// <returns>Uma lista de objetos ImageDTO</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> Get()
        {
            var images = await _imageService.Get();

            if (images.Count() == 0) { return NotFound("Nenhuma imagem encontrada."); }

            return Ok(images);
        }

        // image/{id}
        /// <summary>
        /// Obtém uma imagem específica por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto ImageDTO</returns>
        [HttpGet("{id:int}", Name = "ObterImage")]
        public async Task<ActionResult<ImageDTO>> GetById(Guid id)
        {
            var image = await _imageService.GetById(id);

            return Ok(image);
        }

        // image/
        /// <summary>
        /// Inclui uma nova imagem.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     
        ///     POST /image
        ///     {
        ///         "url": "Url da imagem",
        ///         "bikeId": "Id da bicicleta"
        ///     }
        /// </remarks>
        /// <param name="imageDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(ImageDTO imageDto)
        {
            if (imageDto is null) { return BadRequest("O corpo da requisição não pode ser nulo."); }

            await _imageService.Add(imageDto);

            return Ok("Imagem registrada com sucesso!");
        }

        // image/{id}
        /// <summary>
        /// Atualiza uma imagem.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageDto"></param>
        /// <returns>Um objeto ImageDTO atualizado</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Guid id, ImageDTO imageDto)
        {
            if (id != imageDto.ImageId) { return BadRequest("O ID digitado no body não confere com o o ID fornecido na rota."); }

            await _imageService.Update(imageDto);

            return Ok(imageDto);
        }

        // image/{id}
        /// <summary>
        /// Remove uma imagem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var image = await _imageService.GetById(id);
            if (image is null) { return NotFound("Não foi encontrado uma imagem para o ID fornecido."); }

            await _imageService.Delete(id);

            return Ok("Imagem removida com sucesso!");
        }
    }
}
