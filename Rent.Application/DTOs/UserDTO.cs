using Rents.Domain.Entities;
using System.Collections.ObjectModel;

namespace Rents.Application.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
            Rent = new Collection<RentDTO>();
        }
        public Guid UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string? Password { get; set; }

        public ICollection<RentDTO>? Rent { get; set; }
    }
}
