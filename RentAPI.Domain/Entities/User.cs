using Microsoft.EntityFrameworkCore;
using RentAPI.Validations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rents.Domain.Entities
{
    [Table("user")]
    [Index(nameof(UserEmail), IsUnique = true)]
    public class User
    {
        public User()
        {
            Rent = new Collection<Rent>();
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "O username deve conter no maximo {1} e no minimo {2} caracteres.", MinimumLength = 2)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "O email deve conter no maximo {1} e no minimo {2} caracteres.", MinimumLength = 5)]
        [FormatoEmail]
        public string? UserEmail { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "A senha deve conter no maximo {1} e no minimo {2} caracteres.", MinimumLength = 5)]
        [CaractereEspecial]
        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public ICollection<Rent> Rent { get; set; }
    }
}
