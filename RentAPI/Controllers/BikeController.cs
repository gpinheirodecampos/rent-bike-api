﻿using AutoMapper;
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

namespace Rents.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        // bike/
        /// <summary>
        /// Obtém todas as bikes cadastradas
        /// </summary>
        /// <returns>Uma lista de objeetos BikeDTO</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDTO>>> Get()
        {
            var bikes = await _bikeService.Get();

            if (bikes.Count() == 0) { return NotFound("Não há bikes cadastradas."); }

            return Ok(bikes);
        }

        // bike/{id}
        /// <summary>
        /// Obtém uma bike específica pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto BikeDTO</returns>
        [HttpGet("{id:Guid}", Name = "ObterBike")]
        public async Task<ActionResult<BikeDTO>> GetById(Guid id)
        {
            var bike = await _bikeService.GetById(id);

            return Ok(bike);
        }

        // bike/
        /// <summary>
        /// Inclui uma nova bike
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     
        ///     POST /bike
        ///     {
        ///         "name": "Nome da bike",
        ///         "description": "Descrição da bike",
        ///         "available": 1 para Disponível, 0 para Indisponível,
        ///         "typeBike": 1 para Nova, 0 para Usada
        ///     }
        /// </remarks>
        /// <param name="bikeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(BikeDTO bikeDto)
        {
            if (bikeDto is null) { return BadRequest(); }

            var bike = await _bikeService.Add(bikeDto);

            if (bike is null) { return BadRequest(); }

            return Ok("Bike registrada com sucesso!");
        }

        // bike/{id}
        /// <summary>
        /// Atualiza uma bike
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bikeDto"></param>
        /// <returns>Retorna objeto BikeDTO atualizado</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, BikeDTO bikeDto)
        {
            if (id != bikeDto.BikeId) { return BadRequest("O ID digitado no body não confere com o o ID fornecido na rota."); }

            await _bikeService.Update(bikeDto);

            return Ok(bikeDto);
        }

        // bike/{id}
        /// <summary>
        /// Remove uma bike
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
