using AutoMapper;
using InspectionService.Models;
using InspectionService.Dtos;

namespace InspectionService.Profiles
{
    public class InspectionProfile : Profile
    {
        public InspectionProfile()
        {
            CreateMap<Inspection, InspectionReadDto>();
            CreateMap<InspectionCreateDto, Inspection>();
            CreateMap<InspectionUpdateDto, Inspection>();
        }
    }
}