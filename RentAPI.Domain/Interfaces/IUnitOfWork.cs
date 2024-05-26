namespace Rents.Infrastructure.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRentRepository RentRepository { get; }

        IUserRepository UserRepository { get; }

        IBikeRepository BikeRepository { get; }

        IImageRepository ImageRepository { get; }

        Task Commit();
    }
}
