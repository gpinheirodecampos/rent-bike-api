﻿using Rents.Domain.Entities;

namespace Rents.Application.DTOs
{
    public class RentDTO
    {
        public Guid? RentId { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime? DateStart { get; set; }

        public Guid? BikeId { get; set; }

        public Guid? UserId { get; set; }
    }
}
