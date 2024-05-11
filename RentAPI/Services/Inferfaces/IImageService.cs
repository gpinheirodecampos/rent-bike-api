using RentAPI.DTOs;

namespace RentAPI.Services.Inferfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDTO>> Get();
        Task<ImageDTO> GetById(Guid id);
        Task<ImageDTO> GetByUrl(string url);
        Task<ImageDTO> Add(ImageDTO imageDto);
        Task Update(ImageDTO imageDto);
        Task Delete(Guid id);
    }
}
