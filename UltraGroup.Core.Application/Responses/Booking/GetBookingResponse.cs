namespace UltraGroup.Core.Application.Responses.Booking
{
    public record GetBookingResponse(int RoomdId, string Stardate, string EndDate, int Clients)
    {
        public GetBookingResponse() : this(0, string.Empty, string.Empty, 0) { }
    }
}
