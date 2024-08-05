namespace UltraGroup.Core.Application.Responses.Client
{
    public record GetClientResponse(string FullName, string BirthDate, string Gender, GetDocumentTypeResponse documentType, string Document, string Email, string Phone)
    {
        public GetClientResponse() : this(string.Empty, string.Empty, string.Empty, new GetDocumentTypeResponse(), string.Empty, string.Empty, string.Empty) { }
    }
}
