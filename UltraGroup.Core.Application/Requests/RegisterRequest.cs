namespace UltraGroup.Core.Application.Requests
{
    public record RegisterRequest(string Email, string Password, string PhoneNumber)
    {
        public RegisterRequest() : this(string.Empty, string.Empty, string.Empty) { }
    }
}
