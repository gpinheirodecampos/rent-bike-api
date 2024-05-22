using RentAPI.Context;
using RentAPI.Repository.Interfaces;

namespace RentAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private RentRepository _rentRepo;

        private UserRepository _userRepo;

        private ImageRepository _imageRepo;

        private BikeRepository _bikeRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IRentRepository RentRepository
        { 
            get 
            {
                return _rentRepo = _rentRepo ?? new RentRepository(_context);
            } 
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepo = _userRepo ?? new UserRepository(_context);
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                return _imageRepo = _imageRepo ?? new ImageRepository(_context);
            }
        }

        public IBikeRepository BikeRepository
        {
            get
            {
                return _bikeRepo = _bikeRepo ?? new BikeRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
