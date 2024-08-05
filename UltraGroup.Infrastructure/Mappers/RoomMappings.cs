using AutoMapper;
using AutoMapper.Configuration;
using UltraGroup.Core.Application.Requests.Rooms;
using UltraGroup.Core.Application.Responses.Room;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Mappers
{
    public class RoomMappings : Profile
    {
        public RoomMappings()
        {
            CreateMap<CreateUpdateRoomRequest, RoomEntity>()
                .ForMember(src => src.Room_Descripcion, opt => opt.MapFrom(dest => dest.Description))
                .ForMember(src => src.Room_Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.Room_Tax, opt => opt.MapFrom(dest => dest.Tax))
                .ForMember(src => src.IsReserved, opt => opt.MapFrom(dest => dest.IsReserved))
                .ForMember(src => src.Room_Location, opt => opt.MapFrom(dest => dest.Location))
                .ForMember(src => src.Hotel_ID, opt => opt.MapFrom(dest => dest.IdHotel))
                .ForMember(src => src.TpRoom_ID, opt => opt.MapFrom(dest => dest.IdType))
                .ForMember(src => src.Room_IsAvailable, opt => opt.MapFrom(dest => dest.IsAvailable))
                .ReverseMap();

            CreateMap<RoomEntity, CreateUpdateRoomResponse>()
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Room_Descripcion))
                .ForMember(src => src.Identifier, opt => opt.MapFrom(dest => dest.ID))
                .ReverseMap();

            CreateMap<RoomEntity, GetRoomResponse>()
                .ForMember(src => src.Identifier, opt => opt.MapFrom(dest => dest.ID))
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Room_Descripcion))
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Room_Price))
                .ForMember(src => src.Tax, opt => opt.MapFrom(dest => dest.Room_Tax))
                .ForMember(src => src.IsReserved, opt => opt.MapFrom(dest => dest.IsReserved))
                .ForMember(src => src.Hotel, opt => opt.MapFrom(dest => dest.Hotel))
                .ForMember(src => src.Booking, opt => opt.MapFrom(dest => dest.Booking))
                .ForMember(src => src.Type, opt => opt.MapFrom(dest => dest.TP_Room))
                .ForMember(src => src.Total, opt => opt.MapFrom<CustomTotalMap>())
                .ReverseMap();
        }

        internal class CustomTotalMap : IValueResolver<RoomEntity, GetRoomResponse, decimal>
        {
            public decimal Resolve(RoomEntity source, GetRoomResponse destination, decimal destMember, ResolutionContext context)
            {
                return source.Room_Tax + source.Room_Price;
            }
        }
    }
}
