using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentAPI.DTOs;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;
using RentAPI.Services.Inferfaces;

namespace RentAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageDTO>> Get()
        {
            var images = await _unityOfWork.ImageRepository.Get().ToListAsync() ?? throw new Exception("Imagem não encontrada."); ;

            return _mapper.Map<IEnumerable<ImageDTO>>(images);
        }

        public async Task<ImageDTO> GetById(Guid id)
        {
            var image = await _unityOfWork.ImageRepository.GetByIdAsync(i => i.ImageId == id) ?? throw new Exception("Imagem não encontrada.");

            return _mapper.Map<ImageDTO>(image);
        }

        public async Task<ImageDTO> GetByUrl(string url)
        {
            var image = await _unityOfWork.ImageRepository.GetImageByUrl(i => i.Url == url) ?? throw new Exception("Imagem não encontrada."); ;

            return _mapper.Map<ImageDTO>(image);
        }

        public async Task<ImageDTO> Add(ImageDTO imageDto)
        {
            var imageExists = await _unityOfWork.ImageRepository.GetImageByUrl(i => i.Url == imageDto.Url);
            if (imageExists != null) { throw new Exception("Imagem já cadastrada."); }

            var image = _mapper.Map<Image>(imageDto);
            _unityOfWork.ImageRepository.Add(image);
            await _unityOfWork.Commit();

            return _mapper.Map<ImageDTO>(image);
        }

        public async Task Update(ImageDTO imageDto)
        {
            var image = await _unityOfWork.ImageRepository.GetByIdAsync(i => i.ImageId == imageDto.ImageId) ?? throw new Exception("Imagem não encontrada.");

            _mapper.Map(imageDto, image);
            _unityOfWork.ImageRepository.Update(image);

            await _unityOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var image = await _unityOfWork.ImageRepository.GetByIdAsync(i => i.ImageId == id) ?? throw new Exception("Imagem não encontrada.");
            _unityOfWork.ImageRepository.Delete(image);
            await _unityOfWork.Commit();
        }
    }
}
