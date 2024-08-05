namespace UltraGroup.Core.Application.Requests.Hotel
{
    public record EnableDisableHotelRequest(int HotelId, bool Enabled)
    {
        public EnableDisableHotelRequest() : this(0, false) { }
    }
}
