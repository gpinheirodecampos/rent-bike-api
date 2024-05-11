using RentAPI.Models;
using System.Linq.Expressions;

namespace RentAPI.Repository.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<Image> GetImageByUrl(Expression<Func<Image, bool>> predicate);
    }
}
