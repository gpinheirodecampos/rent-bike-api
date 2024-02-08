using RentAPI.Models;

namespace RentAPI.DTOs
{
    public class ImageDTO
    {
        public int ImageId { get; set; }

        public string? Url { get; set; }

        public int? BikeId { get; set; }
    }
}
