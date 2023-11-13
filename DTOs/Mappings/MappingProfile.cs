using AutoMapper;
using RentAPI.Models;

namespace RentAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
