using Microsoft.EntityFrameworkCore;
using RentAPI.Context;
using RentAPI.Models;
using RentAPI.Repository.Interfaces;
using System.Linq.Expressions;

namespace RentAPI.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext contexto) : base(contexto)
        {

        }

        public async Task<Image> GetImageByUrl(Expression<Func<Image, bool>> predicate)
        {
            return await _context.Images.AsNoTracking().FirstOrDefaultAsync(predicate);
        }
    }
}
