using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // user/
        /// <summary>
        /// Obtém uma lista de usuários cadastrados
        /// </summary>
        /// <returns>Uma lista de objetos UserDTO</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _userService.Get();

            if (users.Count() == 0) { return NotFound("Não há usuários cadastrados."); }

            return Ok(users);
        }

        // user/{id}
        /// <summary>
        /// Obtém um usuário específico por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto UserDTO</returns>
        [HttpGet("{id:Guid}", Name = "ObterUser")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user is null) { return NotFound("Usuário não encontrado."); }

            return Ok(user);
        }

        // user/{email}
        /// <summary>
        /// Obtém um usuário por email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Um objeto UserDTO</returns>
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);

            if (user is null) { return NotFound("Usuário não encontrado."); }

            return Ok(user);
        }

        // user/
        /// <summary>
        /// Inclui um novo usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     
        ///     POST /user
        ///     {
        ///         "userName": "Nome do usuário",
        ///         "userEmail": "Email do usuário",
        ///         "password": "Senha do usuário"
        ///     }
        /// </remarks>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult> Post(UserDTO userDto)
        {
            if (userDto is null) { return BadRequest("Body não informado."); }

            await _userService.Add(userDto);

            return Ok("User registrado com sucesso!");
        }

        // user/{id}
        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns>Um objeto UserDTO atualizado</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, UserDTO userDto)
        {
            if (id != userDto.UserId) { return BadRequest(); }

            await _userService.Update(userDto);

            return Ok(userDto);
        }

        // user/{id}
        /// <summary>
        /// Remove um usuário
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user is null) { return NotFound("Usuário não encontrado."); }

            await _userService.Delete(id);

            return Ok("Usuário removido com sucesso!");
        }
    }
}
