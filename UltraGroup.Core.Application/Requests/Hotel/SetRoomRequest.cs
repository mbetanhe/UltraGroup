namespace UltraGroup.Core.Application.Requests.Hotel
{
    public record SetRoomRequest(int HotelId, int RoomId, List<int> RoomIds)
    {
        public SetRoomRequest() : this(0, 0, new List<int>()) { }
    }
}
