using RentAPI.Context;
using RentAPI.Models;

namespace RentAPI.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
