namespace UltraGroup.Core.Application.Requests.Rooms
{
    public record CreateUpdateRoomRequest(int Id, string Description, decimal Price, decimal Tax, bool IsReserved, string Location, int IdHotel, int IdType, bool IsAvailable)
    {
        public CreateUpdateRoomRequest() : this(0, string.Empty, 0, 0, false, string.Empty, 0, 0, false) { }
    }
}
