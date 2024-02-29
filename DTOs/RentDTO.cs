using RentAPI.Models;

namespace RentAPI.DTOs
{
    public class RentDTO
    {
        public Guid? RentId { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime? DateStart { get; set; }

        public Bike? Bike { get; set; } = null!;

        public Guid? BikeId { get; set; }

        public User? User { get; set; } = null!;

        public Guid? UserId { get; set; }
    }
}
