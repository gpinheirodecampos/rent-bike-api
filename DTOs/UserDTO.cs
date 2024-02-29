using RentAPI.Models;

namespace RentAPI.DTOs
{
    public class UserDTO
    {
        public Guid? UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string? Password { get; set; }

        public ICollection<Rent>? Rent { get; set; }
    }
}
