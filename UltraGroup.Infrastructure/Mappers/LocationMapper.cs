using AutoMapper;
using UltraGroup.Core.Application.Requests;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<LocationEntity, GetLocationRequest>()
                .ForMember(src => src.Id, opt => opt.MapFrom(dest => dest.ID))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Description))
                .ReverseMap();
        }
    }
}
