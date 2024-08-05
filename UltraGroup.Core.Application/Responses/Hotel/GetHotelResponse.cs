using UltraGroup.Core.Application.Requests;

namespace UltraGroup.Core.Application.Responses.Hotel
{
    public record GetHotelResponse(int Identifier, string Name, string Description, string Address, int Capacity, bool Available, string UserID, GetLocationRequest Location)
    {
        public GetHotelResponse() : this(0, string.Empty, string.Empty, string.Empty, 0, false, string.Empty, new GetLocationRequest()) { }
    }
}
