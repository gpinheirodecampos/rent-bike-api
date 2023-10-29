﻿namespace RentBikeApi.Models
{
    public class Rent
    {
        public int RentId { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime? DateStart { get; set; }

        public Bike Bike { get; set; } = null!;

        public int BikeId { get; set; }

        public User User { get; set; } = null!;

        public int UserId { get; set; }
    }
}
