using AutoMapper;

namespace DotnetTaskAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramDetails, ProgramDetailsDTO>().ReverseMap();
        }
    }
}