using AutoMapper;
using UniversityService.DTOs;
using UniversityService.Models;

namespace UniversityService.Profiles
{
    public class UniversitysProfile : Profile
    {
        public UniversitysProfile()
        {
            // Source -> Target
            CreateMap<University, UniversityReadDto>();
            CreateMap<UniversityCreateDto, University>();
            CreateMap<UniversityReadDto, UniversityPublishedDto>();
            CreateMap<University, GrpcUniversityModel>()
                .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src =>src.Publisher));
        }
    }
}