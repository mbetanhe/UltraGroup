namespace UltraGroup.Core.Application.Requests
{
    public record GetLocationRequest(int Id, string Name)
    {
        public GetLocationRequest() : this(0, string.Empty) { }
    }
}
