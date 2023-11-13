using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.DTOs;
using RentAPI.Filters;
using RentAPI.Models;
using RentAPI.Repository;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;

        public UserController(IUnityOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // user/
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _context.UserRepository.Get().ToListAsync();
            var usersDto = _mapper.Map<List<UserDTO>>(users);

            if (usersDto is null) { return NotFound("Nao ha usuarios cadastrados."); }

            return usersDto;
        }

        // user/{id}
        [HttpGet("{id:int}", Name = "ObterUser")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _context.UserRepository.GetByIdAsync(u => u.UserId == id);
            var userDto = _mapper.Map<UserDTO>(user);

            if (userDto is null) { return NotFound("Usuario nao encontrado."); }

            return userDto;
        }

        // user/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> Get(string email)
        {
            var user = await _context.UserRepository.GetUserByEmail(u => u.UserEmail == email);
            var userDto = _mapper.Map<UserDTO>(user);

            if (userDto is null) { return NotFound("Usuario nao encontrado."); }

            return userDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (user is null) { return BadRequest(); }

            _context.UserRepository.Add(user);
            await _context.Commit();

            var userDTO = _mapper.Map<UserDTO>(user);

            return new CreatedAtRouteResult("ObterUser", 
                new { id = user.UserId }, userDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (id != user.UserId) { return BadRequest(); }

            _context.UserRepository.Update(user);
            await _context.Commit();

            return Ok(userDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.UserRepository.GetByIdAsync(u => u.UserId == id);

            if (user is null) { return NotFound("Usuario nao encontrado."); }

            _context.UserRepository.Delete(user);
            await _context.Commit();

            return Ok();
        }
    }
}
