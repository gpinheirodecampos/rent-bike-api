namespace RentBikeApi.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string? Url { get; set; }
        public Bike? Bike { get; set; } = null!;
        public int? BikeId { get; set; }

    }
}
