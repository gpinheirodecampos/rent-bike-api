using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentBikeApi.Models
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
        [StringLength(80)]
        public string? Name { get; set; }

        [Required]
        [StringLength(300)]
        public string? Description { get; set; }

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
