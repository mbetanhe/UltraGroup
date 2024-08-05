using UltraGroup.Core.Application.Requests.Client;

namespace UltraGroup.Core.Application.Requests.Bookings
{
    public record CreateBookingRequest(int RoomId, string Stardate, string EndDate, string FullNameEmergency, string PhoneEmergency, List<CreateClientRequest> clients)
    {
        public CreateBookingRequest() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, new List<CreateClientRequest>()) { }
    }
}
