namespace RentAPI.Repository
{
    public interface IUnityOfWork
    {
        IRentRepository RentRepository { get; }

        IUserRepository UserRepository { get; }

        IBikeRepository BikeRepository { get; }

        IImageRepository ImageRepository { get; }

        Task Commit();
    }
}
