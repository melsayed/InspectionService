using AutoMapper;
using InspectionService.Dtos;
using InspectionService.Models;

namespace InspectionService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}