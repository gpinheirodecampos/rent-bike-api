using System;

namespace RentBikeApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        [Index(nameof(UserEmail), IsUnique = true)]
        public string? UserEmail { get; set; }

        public string? Password { get; set; }

        public ICollection<Rent>? Rent { get; set; }
    }
}
