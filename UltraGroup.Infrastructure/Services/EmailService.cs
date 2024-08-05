using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Domain.Settings;

namespace UltraGroup.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        private readonly EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger) => (_settings, _logger) = (options.Value, logger);

        public async Task<bool> SendMailAsync(string to, string subject, string clientName)
        {
            using (MailMessage mensajeMail = new MailMessage())
            {
                try
                {

                    mensajeMail.From = new System.Net.Mail.MailAddress(_settings.Email, _settings.DsiplayName);
                    mensajeMail.To.Add(to);
                    mensajeMail.Subject = subject;
                    mensajeMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    mensajeMail.Body = $"Reservation has been registered for cclient {clientName}";
                    mensajeMail.BodyEncoding = System.Text.Encoding.UTF8;
                    mensajeMail.IsBodyHtml = true;
                    mensajeMail.Priority = System.Net.Mail.MailPriority.Normal;

                    using (SmtpClient client = new SmtpClient())
                    {
                        #region PA
                        client.Credentials = new System.Net.NetworkCredential(_settings.Email, _settings.Password);
                        #endregion
                        client.Port = _settings.Port;//--int.Parse(puerto);-
                        client.Host = _settings.Server;
                        client.EnableSsl = _settings.enableSsl;
                        client.Timeout = 60000;
                        await client.SendMailAsync(mensajeMail);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} => {ex.Message}");
                    return false;
                }
            }
        }

    }
}
