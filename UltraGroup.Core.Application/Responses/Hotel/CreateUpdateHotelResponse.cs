namespace UltraGroup.Core.Application.Responses.Hotel
{
    public record CreateUpdateHotelResponse(int Id, string Name)
    {
        public CreateUpdateHotelResponse() : this(0, string.Empty) { }
    }
}
