using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;
using RentAPI.Services.Inferfaces;

namespace RentAPI.Services
{
    public class RentService : IRentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;

        public RentService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RentDTO>> Get()
        {
            var rents = await _UnitOfWork.RentRepository.Get().ToListAsync();

            return _mapper.Map<IEnumerable<RentDTO>>(rents);
        }

        public async Task<RentDTO> GetById(Guid id)
        {
            var rent = await _UnitOfWork.RentRepository.GetByProperty(x => x.RentId == id);

            return _mapper.Map<RentDTO>(rent);
        }

        public async Task<RentDTO> GetByUserEmail(string email)
        {
            var user = await _UnitOfWork.UserRepository.GetByProperty(x => x.UserEmail == email);

            var rent = await _UnitOfWork.RentRepository.GetByProperty(x => x.UserId == user.UserId);

            return _mapper.Map<RentDTO>(rent);
        }

        public async Task<RentDTO> GetByBikeId(Guid id)
        {
            var bike = await _UnitOfWork.BikeRepository.GetByProperty(x => x.BikeId == id);

            var rent = await _UnitOfWork.RentRepository.GetByProperty(x => x.BikeId == bike.BikeId);

            return _mapper.Map<RentDTO>(rent);
        }

        public async Task Add(RentDTO rentDto)
        {
            var bike = await _UnitOfWork.BikeRepository.GetByProperty(x => x.BikeId == rentDto.BikeId) ?? throw new Exception("Bicicleta não encontrada.");

            if (!bike.Available) { throw new Exception("Bicicleta já alugada."); }

            var user = await _UnitOfWork.UserRepository.GetByProperty(x => x.UserId == rentDto.UserId) ?? throw new Exception("Usuário não encontrado.");

            rentDto.DateStart = DateTime.Now;

            var rent = _mapper.Map<Rent>(rentDto);

            _UnitOfWork.RentRepository.Add(rent);

            bike.Available = false;

            _UnitOfWork.BikeRepository.Update(bike);

            await _UnitOfWork.Commit();
        }

        public async Task Update(RentDTO rentDto)
        {
            var rent = _mapper.Map<Rent>(rentDto);

            _UnitOfWork.RentRepository.Update(rent);

            await _UnitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var rent = await _UnitOfWork.RentRepository.GetByProperty(x => x.RentId == id);

            _UnitOfWork.RentRepository.Delete(rent);

            await _UnitOfWork.Commit();
        }
    }
}
