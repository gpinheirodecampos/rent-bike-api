using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.DTOs;
using RentAPI.Filters;
using RentAPI.Models;
using RentAPI.Repository;
using RentAPI.Services.Inferfaces;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // user/
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _userService.Get();

            if (users.Count() == 0) { return NotFound("Não há usuários cadastrados."); }

            return Ok(users);
        }

        // user/{id}
        [HttpGet("{id:Guid}", Name = "ObterUser")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null) { return NotFound("Usuário não encontrado."); }

            return Ok(user);
        }

        // user/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> Get(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user is null) { return NotFound("Usuário não encontrado."); }

            return Ok(user);
        }

        // user/
        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult> Post(UserDTO userDto)
        {
            var user = await _userService.AddAsync(userDto);

            if (user is null) { return BadRequest(); }

            return Ok("User registrado com sucesso!");
        }

        // user/{id}
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, UserDTO userDto)
        {
            if (id != userDto.UserId) { return BadRequest(); }

            await _userService.UpdateAsync(userDto);

            return Ok(userDto);
        }

        // user/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null) { return NotFound("Usuário não encontrado."); }

            await _userService.DeleteAsync(id);

            return Ok("Usuário removido com sucesso!");
        }
    }
}
