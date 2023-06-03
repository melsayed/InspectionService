using AutoMapper;
using InspectionService.Dtos;
using InspectionService.Models;

namespace InspectionService.Profiles
{
    public class InspectionTypeProfile : Profile
    {
        public InspectionTypeProfile()
        {
            CreateMap<InspectionType, InspectionTypeReadDto>();
        }
    }
}