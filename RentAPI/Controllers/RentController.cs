using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rents.Infrastructure.Context;
using Rents.Application.DTOs;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;
using Rents.Application.Services.Inferfaces;
using System.Formats.Asn1;

namespace Rents.Api.Controllers
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

        [HttpGet("user/{email}", Name = "ObterRentPorEmailDoUsuario")]
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

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, RentDTO rentDto)
        {
            if (id != rentDto.RentId) { return BadRequest("Id da rent não corresponde ao Id da requisição."); }

            await _rentService.Update(rentDto);

            return Ok(rentDto);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var rent = await _rentService.GetById(id);

            if (rent is null) { return NotFound("Rent não encontrada."); }

            await _rentService.Delete(id);

            return Ok("Rent removido com sucesso!");
        }
    }
}
