using System.ComponentModel;

namespace RentBikeApi.Models
{
    public class Bike
    {
        public int BikeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public TypeBike TypeBike { get; set; }

        public ICollection<Image> Images { get; } = new List<Image>();

    }

    public enum TypeBike
    {
        [Description("New")]
        New = 1,

        [Description("Used")]
        Used = 2
    }
}
