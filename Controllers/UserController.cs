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

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnityOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnityOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }

        // user/
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _unitOfWork.UserRepository.Get().ToListAsync();
            var usersDto = _mapper.Map<List<UserDTO>>(users);

            if (usersDto is null) { return NotFound("Nao ha usuarios cadastrados."); }

            return usersDto;
        }

        // user/{id}
        [HttpGet("{id:int}", Name = "ObterUser")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(u => u.UserId == id);
            var userDto = _mapper.Map<UserDTO>(user);

            if (userDto is null) { return NotFound("Usuario nao encontrado."); }

            return userDto;
        }

        // user/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> Get(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(u => u.UserEmail == email);
            var userDto = _mapper.Map<UserDTO>(user);

            if (userDto is null) { return NotFound("Usuario nao encontrado."); }

            return userDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (user is null) { return BadRequest(); }

            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Commit();

            var userDTO = _mapper.Map<UserDTO>(user);

            return new CreatedAtRouteResult("ObterUser", 
                new { id = user.UserId }, userDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (id != user.UserId) { return BadRequest(); }

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            return Ok(userDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(u => u.UserId == id);

            if (user is null) { return NotFound("Usuario nao encontrado."); }

            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.Commit();

            return Ok();
        }
    }
}
