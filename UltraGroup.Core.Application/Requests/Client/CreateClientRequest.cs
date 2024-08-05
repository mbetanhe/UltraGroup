namespace UltraGroup.Core.Application.Requests.Client
{
    public record CreateClientRequest(string FullName, string BirthDate, string Gender, int DocumentTypeId, string Document, string Email, string Phone)
    {
        public CreateClientRequest() : this(string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty) { }
    }
}
