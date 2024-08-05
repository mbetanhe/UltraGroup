using UltraGroup.Core.Application.Responses.Client;
using UltraGroup.Core.Application.Responses.Room;

namespace UltraGroup.Core.Application.Responses.Booking
{
    public record GetBookingDetailResponse(int BookingId, string Stardate, string EndDate, string FullNameEmergency, string PhoneEmergency, List<GetClientResponse> Clients, GetRoomResponse Room, decimal Total)
    {
        public GetBookingDetailResponse() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, new List<GetClientResponse>(), new GetRoomResponse(), 0) { }
    }
}
