using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentBikeApi.Models
{
    [Table("User")]
    [Index(nameof(UserEmail), IsUnique = true)]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(80)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(80)]
        public string? UserEmail { get; set; }

        [Required]
        [StringLength(80)]
        public string? Password { get; set; }

        public ICollection<Rent>? Rent { get; set; }
    }
}
