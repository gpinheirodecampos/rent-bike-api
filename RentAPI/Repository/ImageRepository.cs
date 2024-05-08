using RentAPI.Context;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;

namespace RentAPI.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
