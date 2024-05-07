using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RentAPI.Models
{
    [Table("image")]
    public class Image
    {
        [Key]
        public Guid ImageId { get; set; }

        [Required]
        [StringLength(300)]
        public string? Url { get; set; }

        [JsonIgnore]
        public Bike? Bike { get; } = null!;

        public Guid? BikeId { get; set; }

    }
}
