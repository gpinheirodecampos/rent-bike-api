using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rents.Application.DTOs;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;
using Rents.Application.Services.Inferfaces;

namespace Rents.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageDTO>> Get()
        {
            var images = await _UnitOfWork.ImageRepository.Get().ToListAsync() ?? throw new Exception("Imagem não encontrada."); ;

            return _mapper.Map<IEnumerable<ImageDTO>>(images);
        }

        public async Task<ImageDTO> GetById(Guid id)
        {
            var image = await _UnitOfWork.ImageRepository.GetByProperty(i => i.ImageId == id) ?? throw new Exception("Imagem não encontrada.");

            return _mapper.Map<ImageDTO>(image);
        }

        public async Task<ImageDTO> GetByUrl(string url)
        {
            var image = await _UnitOfWork.ImageRepository.GetImageByUrl(i => i.Url == url) ?? throw new Exception("Imagem não encontrada."); ;

            return _mapper.Map<ImageDTO>(image);
        }

        public async Task<ImageDTO> Add(ImageDTO imageDto)
        {
            var imageExists = await _UnitOfWork.ImageRepository.GetImageByUrl(i => i.Url == imageDto.Url);
            if (imageExists != null) { throw new Exception("Imagem já cadastrada."); }

            var image = _mapper.Map<Image>(imageDto);
            _UnitOfWork.ImageRepository.Add(image);
            await _UnitOfWork.Commit();

            return _mapper.Map<ImageDTO>(image);
        }

        public async Task Update(ImageDTO imageDto)
        {
            var image = await _UnitOfWork.ImageRepository.GetByProperty(i => i.ImageId == imageDto.ImageId) ?? throw new Exception("Imagem não encontrada.");

            _mapper.Map(imageDto, image);
            _UnitOfWork.ImageRepository.Update(image);

            await _UnitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var image = await _UnitOfWork.ImageRepository.GetByProperty(i => i.ImageId == id) ?? throw new Exception("Imagem não encontrada.");
            _UnitOfWork.ImageRepository.Delete(image);
            await _UnitOfWork.Commit();
        }
    }
}
