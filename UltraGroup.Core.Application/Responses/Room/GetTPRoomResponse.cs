namespace UltraGroup.Core.Application.Responses.Room
{
    public record GetTPRoomResponse(int Identifier, string Description)
    {
        public GetTPRoomResponse() : this(0, string.Empty) { }
    }
}
