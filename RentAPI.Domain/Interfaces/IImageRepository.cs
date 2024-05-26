using Rents.Domain.Entities;
using System.Linq.Expressions;

namespace Rents.Infrastructure.Repository.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<Image> GetImageByUrl(Expression<Func<Image, bool>> predicate);
    }
}
