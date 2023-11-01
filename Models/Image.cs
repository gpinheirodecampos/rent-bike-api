using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentBikeApi.Models
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        [StringLength(300)]
        public string? Url { get; set; }
        public Bike? Bike { get; set; } = null!;
        public int? BikeId { get; set; }

    }
}
