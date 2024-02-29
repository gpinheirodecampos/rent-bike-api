using RentAPI.Models;
using System.Collections.ObjectModel;
using static RentAPI.Enums.Enum;

namespace RentAPI.DTOs
{
    public class BikeDTO
    {
        public BikeDTO() 
        {
            Images = new Collection<ImageDTO>();
        }

        public Guid? BikeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? Available { get; set; }

        public TypeBike? TypeBike { get; set; }

        public ICollection<ImageDTO>? Images { get; set; }
    }
}
