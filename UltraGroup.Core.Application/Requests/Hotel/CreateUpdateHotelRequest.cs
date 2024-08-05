namespace UltraGroup.Core.Application.Requests.Hotel
{
    public record CreateUpdateHotelRequest(int Id, string Name, string Description, string Address, int Capacity, bool Available, int LocationId, string UserID)
    {
        public CreateUpdateHotelRequest() : this(0,string.Empty, string.Empty, string.Empty,  0, false, 0, string.Empty) { }
    }
}
