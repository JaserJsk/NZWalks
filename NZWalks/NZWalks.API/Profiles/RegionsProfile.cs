using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        RegionsProfile()
        {
            CreateMap<Region, RegionDTO>().ForMember(dest => dest.RegionId, options => options.MapFrom(src => src.Id));
        }
    }
}
