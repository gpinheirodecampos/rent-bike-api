using RentAPI.Validations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    [Table("Bike")]
    public class Bike
    {
        public Bike() 
        {
            Images = new Collection<Image>();
        }

        [Key]
        public int BikeId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "O nome da bike deve conter no maximo {1} e no minimo {2} caracteres.", MinimumLength = 4)]
        [PrimeiraLetraMaiuscula]
        public string? Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "A descricao deve conter no maximo {1} e no minimo {2} caracteres.", MinimumLength = 5)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Informe o tipo da bike: 1 para NOVA, 2 para USADA")]
        [Range(1, 2, ErrorMessage = "Informe 1 para NOVA, 2 para USADA")]
        public TypeBike TypeBike { get; set; }

        public ICollection<Image> Images { get; }

    }

    public enum TypeBike
    {
        [Description("New")]
        New = 1,

        [Description("Used")]
        Used = 2
    }
}
