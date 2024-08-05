
namespace UltraGroup.Core.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(string to, string subject, string clientName);
    }
}