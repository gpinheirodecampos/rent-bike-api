using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository;
using System.Formats.Asn1;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IUnityOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RentController(IUnityOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentDTO>>> GetAsync()
        {
            var rents = await _unitOfWork.RentRepository.Get().ToListAsync();
            var rentsDto = _mapper.Map<List<RentDTO>>(rents);

            if (rentsDto is null) { return NotFound("Nao ha rents cadastradas."); }

            return rentsDto;
        }

        [HttpGet("{id:int}", Name = "ObterRent")]
        public async Task<ActionResult<RentDTO>> GetAsync(int id)
        {
            var rent = await _unitOfWork.RentRepository.GetByIdAsync(r => r.RentId == id);
            var rentDto = _mapper.Map<RentDTO>(rent);

            if (rentDto is null) { return NotFound("Rent nao encontrada."); }

            return rentDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(RentDTO rentDto)
        {
            if (rentDto is null) { return BadRequest(); }

            var rent = _mapper.Map<Rent>(rentDto);

            _unitOfWork.RentRepository.Add(rent);
            await _unitOfWork.Commit();

            var rentDTO = _mapper.Map<RentDTO>(rent);

            return new CreatedAtRouteResult("ObterRent",
                new { id = rent.RentId }, rentDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, RentDTO rentDto)
        {
            var rent = _mapper.Map<Rent>(rentDto);

            if (id != rent.RentId) { return BadRequest(); }

            _unitOfWork.RentRepository.Update(rent);
            await _unitOfWork.Commit();

            return Ok(rent);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rent = await _unitOfWork.RentRepository.GetByIdAsync(r => r.RentId ==  id);

            if (rent is null) { return NotFound("Rent nao encontrada."); }

            _unitOfWork.RentRepository.Delete(rent);
            await _unitOfWork.Commit();

            return Ok();
        }
    }
}
