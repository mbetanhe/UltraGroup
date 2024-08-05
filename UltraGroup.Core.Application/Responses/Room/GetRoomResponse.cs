using UltraGroup.Core.Application.Responses.Booking;
using UltraGroup.Core.Application.Responses.Hotel;

namespace UltraGroup.Core.Application.Responses.Room
{
    public record GetRoomResponse(int Identifier, string Description, decimal Price, decimal Tax, decimal Total, bool IsReserved, string Location, GetHotelResponse Hotel, GetTPRoomResponse Type, GetBookingResponse Booking)
    {
        public GetRoomResponse() : this(0, string.Empty, 0, 0, 0, false, string.Empty, new GetHotelResponse(), new GetTPRoomResponse(), new GetBookingResponse()) { }
    }
}
