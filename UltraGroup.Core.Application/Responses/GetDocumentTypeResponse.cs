namespace UltraGroup.Core.Application.Responses
{
    public record GetDocumentTypeResponse(int Id, string name)
    {
        public GetDocumentTypeResponse() : this(0, string.Empty) { }
    }
}

