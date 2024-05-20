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
using System.Formats.Asn1;

namespace RentAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentDTO>>> Get()
        {
            var rents = await _rentService.Get();

            if (rents.Count() == 0) { return NotFound("Não há rents cadastradas."); }

            return Ok(rents);
        }

        [HttpGet("{id:Guid}", Name = "ObterRentPorId")]
        public async Task<ActionResult<RentDTO>> GetById(Guid id)
        {
            if (id == Guid.Empty) { return BadRequest("Id não informado."); }

            var rent = await _rentService.GetById(id);

            if (rent is null) { return NotFound("Rent não encontrada."); }

            return Ok(rent);
        }

        [HttpGet("user/{email:string}", Name = "ObterRentPorEmailDoUsuario")]
        public async Task<ActionResult<RentDTO>> GetByUserEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { return BadRequest("Email não informado."); }

            var rent = await _rentService.GetByUserEmail(email);

            if (rent is null) { return NotFound("Rent não encontrada."); }

            return Ok(rent);
        }

        [HttpGet("bike/{id:Guid}", Name = "ObterRentPorIdDaBicicleta")]
        public async Task<ActionResult<RentDTO>> GetByBikeId(Guid id)
        {
            if (id == Guid.Empty) { return BadRequest("Id não informado."); }

            var rent = await _rentService.GetByBikeId(id);

            if (rent is null) { return NotFound("Rent não encontrada."); }

            return Ok(rent);
        }

        [HttpPost]
        public async Task<ActionResult> Post(RentDTO rentDto)
        {
            if (rentDto.UserId is null || rentDto.BikeId is null) { return BadRequest("Body não informado ou incompleto."); }

            await _rentService.Add(rentDto);

            return Ok("Rent registrado com sucesso!");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Guid id, RentDTO rentDto)
        {
            if (id != rentDto.RentId) { return BadRequest("Id da rent não corresponde ao Id da requisição."); }

            await _rentService.Update(rentDto);

            return Ok(rentDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var rent = await _rentService.GetById(id);

            if (rent is null) { return NotFound("Rent não encontrada."); }

            await _rentService.Delete(id);

            return Ok("Rent removido com sucesso!");
        }
    }
}
