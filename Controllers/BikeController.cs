using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository;

namespace RentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IUnityOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BikeController(IUnityOfWork context, IMapper mapper)
        {
            _unitOfWork = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDTO>>> GetAsync()
        {
            var bikes = await _unitOfWork.BikeRepository.Get().ToListAsync();

            var bikesDto = _mapper.Map<List<BikeDTO>>(bikes);

            if (bikesDto is null) { return NotFound("Nao ha bikes cadastradas."); }

            return bikesDto;
        }

        [HttpGet("{id:int}", Name = "ObterBike")]
        public async Task<ActionResult<BikeDTO>> GetAsync(Guid id)
        {
            var bike = await _unitOfWork.BikeRepository.GetByIdAsync(b => b.BikeId == id);

            var bikeDto = _mapper.Map<BikeDTO>(bike);

            if (bikeDto is null) { return NotFound("Bike nao encontrada."); }

            return bikeDto;
        }

        [HttpGet("images")]
        public async Task<ActionResult<IEnumerable<BikeDTO>>> GetBikesImagesAsync()
        {
            var bikes = await _unitOfWork.BikeRepository.GetBikesImages().ToListAsync();

            var bikesDto = _mapper.Map<List<BikeDTO>>(bikes);

            if (bikesDto is null) { return NotFound("Nao ha bikes cadastradas."); }

            return bikesDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(BikeDTO bikeDto)
        {
            if (bikeDto is null) { return BadRequest(); }

            var bike = _mapper.Map<Bike>(bikeDto);

            _unitOfWork.BikeRepository.Add(bike);
            await _unitOfWork.Commit();

            return Ok("Bike registrada com sucesso!");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Guid id, BikeDTO bikeDto)
        {
            if (id != bikeDto.BikeId) { return BadRequest("O id da bike digitada no body nao confere com o id digitado na rota"); }

            var bike = _mapper.Map<Bike>(bikeDto);

            _unitOfWork.BikeRepository.Update(bike);
            await _unitOfWork.Commit();

            return Ok(bikeDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var bike = await _unitOfWork.BikeRepository.GetByIdAsync(b => b.BikeId ==  id);

            if (bike is null) { return NotFound("Bike nao encontrada."); }

            _unitOfWork.BikeRepository.Delete(bike);
            await _unitOfWork.Commit();

            return Ok();
        }
    }
}
