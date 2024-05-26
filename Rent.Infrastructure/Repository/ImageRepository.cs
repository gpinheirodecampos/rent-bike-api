using Microsoft.EntityFrameworkCore;
using Rents.Infrastructure.Context;
using Rents.Domain.Entities;
using Rents.Infrastructure.Repository.Interfaces;
using System.Linq.Expressions;

namespace Rents.Infrastructure.Repository
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
