using Rents.Domain.Entities;

namespace Rents.Application.DTOs
{
    public class ImageDTO
    {
        public Guid? ImageId { get; set; }

        public string? Url { get; set; }

        public Guid? BikeId { get; set; }
    }
}
