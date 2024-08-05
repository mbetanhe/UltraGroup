namespace UltraGroup.Core.Application.Responses.Room
{
    public record CreateUpdateRoomResponse(int Identifier, string Description)
    {
        public CreateUpdateRoomResponse() : this(0, string.Empty) { }
    }
}
