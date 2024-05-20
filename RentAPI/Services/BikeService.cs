using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository;
using RentAPI.Repository.Interfaces;
using RentAPI.Services.Inferfaces;

namespace RentAPI.Services
{
    public class BikeService : IBikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BikeDTO>> Get()
        {
            var bikes = await _unitOfWork.BikeRepository.Get(x => x.Images).ToListAsync();

            return _mapper.Map<List<BikeDTO>>(bikes);
        }

        public async Task<BikeDTO> GetById(Guid id)
        {
            var bike = await _unitOfWork.BikeRepository.GetByProperty(b => b.BikeId == id);

            return _mapper.Map<BikeDTO>(bike);
        }

        public async Task<BikeDTO> Add(BikeDTO bikeDto)
        {
            var bikeExists = await _unitOfWork.BikeRepository.GetByProperty(b => b.BikeId == bikeDto.BikeId);

            if (bikeExists != null) { throw new Exception("Bike já cadastrada."); }

            var bike = _mapper.Map<Bike>(bikeDto);

             _unitOfWork.BikeRepository.Add(bike);

            await _unitOfWork.Commit();

            return bikeDto;
        }

        public async Task Update(BikeDTO bikeDto)
        {
            var bike = _mapper.Map<Bike>(bikeDto);

            _unitOfWork.BikeRepository.Update(bike);

            await _unitOfWork.Commit();
        }

        public async Task Delete(BikeDTO bikeDto)
        {
            var bike = _mapper.Map<Bike>(bikeDto);

            _unitOfWork.BikeRepository.Delete(bike);

            await _unitOfWork.Commit();
        }
    }
}
