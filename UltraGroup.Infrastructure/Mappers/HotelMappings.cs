using AutoMapper;
using UltraGroup.Core.Application.Requests.Hotel;
using UltraGroup.Core.Application.Responses.Hotel;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class HotelMappings : Profile
    {
        public HotelMappings()
        {
            CreateMap<CreateUpdateHotelRequest, HotelEntity>()
            .ForMember(src => src.Htl_Name, opt => opt.MapFrom(dest => dest.Name))
            .ForMember(src => src.Htl_Description, opt => opt.MapFrom(dest => dest.Description))
            .ForMember(src => src.Htl_Address, opt => opt.MapFrom(dest => dest.Address))
            .ForMember(src => src.Htl_IsAvailable, opt => opt.MapFrom(dest => dest.Available))
            .ForMember(src => src.Htl_UsersQuantity, opt => opt.MapFrom(dest => dest.Capacity))
            .ForMember(src => src.CreatedByUserId, opt => opt.MapFrom(dest => dest.UserID))
            .ReverseMap();

            CreateMap<HotelEntity, CreateUpdateHotelResponse>()
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Htl_Name))
                .ForMember(src => src.Id, opt => opt.MapFrom(dest => dest.ID))
                .ReverseMap();

            CreateMap<HotelEntity, GetHotelResponse>()
                .ForMember(src => src.Identifier, opt => opt.MapFrom(dest => dest.ID))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Htl_Name))
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Htl_Description))
                .ForMember(src => src.Address, opt => opt.MapFrom(dest => dest.Htl_Address))
                .ForMember(src => src.Capacity, opt => opt.MapFrom(dest => dest.Htl_UsersQuantity))
                .ForMember(src => src.Available, opt => opt.MapFrom(dest => dest.Htl_IsAvailable))
                .ForMember(src => src.Location, opt => opt.MapFrom(dest => dest.Location))
                .ReverseMap();
        }
    }
}
