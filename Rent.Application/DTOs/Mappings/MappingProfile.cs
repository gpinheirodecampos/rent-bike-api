using AutoMapper;
using Rents.Application.DTOs;
using Rents.Domain.Entities;

namespace Rents.Application.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Rent, RentDTO>().ReverseMap();
            CreateMap<Bike, BikeDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
        }
    }
}
