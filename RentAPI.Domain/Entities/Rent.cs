using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rents.Domain.Entities
{
    [Table("rent")]
    public class Rent
    {
        [Key]
        public Guid RentId { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime? DateStart { get; set; }

        [JsonIgnore]
        public Bike Bike { get; } = null!;

        public Guid BikeId { get; set; }

        [JsonIgnore]
        public User User { get; } = null!;

        public Guid UserId { get; set; }
    }
}
