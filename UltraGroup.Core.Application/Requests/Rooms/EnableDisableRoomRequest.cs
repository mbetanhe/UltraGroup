namespace UltraGroup.Core.Application.Requests.Rooms
{
    public record EnableDisableRoomRequest(int RoomId, bool Enabled)
    {
        public EnableDisableRoomRequest() : this(0, false) { }
    }
}
