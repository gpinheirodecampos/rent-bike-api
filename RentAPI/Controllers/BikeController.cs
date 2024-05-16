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
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDTO>>> Get()
        {
            var bikes = await _bikeService.Get();

            return Ok(bikes);
        }

        [HttpGet("{id:Guid}", Name = "ObterBike")]
        public async Task<ActionResult<BikeDTO>> GetById(Guid id)
        {
            var bike = await _bikeService.GetById(id);

            return Ok(bike);
        }

        [HttpPost]
        public async Task<ActionResult> Post(BikeDTO bikeDto)
        {
            if (bikeDto is null) { return BadRequest(); }

            var bike = await _bikeService.Add(bikeDto);

            if (bike is null) { return BadRequest(); }

            return Ok("Bike registrada com sucesso!");
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, BikeDTO bikeDto)
        {
            if (id != bikeDto.BikeId) { return BadRequest("O ID digitado no body não confere com o o ID fornecido na rota."); }

            await _bikeService.Update(bikeDto);

            return Ok(bikeDto);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var bike = await _bikeService.GetById(id);

            if (bike is null) { return NotFound("Bike não encontrada."); }

            await _bikeService.Delete(bike);

            return Ok("Bike removida com sucesso.");
        }
    }
}
