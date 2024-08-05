using AutoMapper;
using UltraGroup.Core.Application.Responses.Room;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class TpRoomsMappings : Profile
    {
        public TpRoomsMappings()
        {
            CreateMap<TP_RoomEntity, GetTPRoomResponse>()
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.TpRoom_Descripcion))
                .ForMember(src => src.Identifier, opt => opt.MapFrom(dest => dest.ID))
                .ReverseMap();
        }
    }
}
