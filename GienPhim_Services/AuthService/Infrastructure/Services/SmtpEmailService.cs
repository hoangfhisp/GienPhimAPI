using GienPhim_Services.AuthService.Application.Interfaces.Services;
using GienPhim_Services.AuthService.Application.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GienPhim_Services.AuthService.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public SmtpEmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        // Gửi email bất đồng bộ
        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailOptions.Name, _emailOptions.Email));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, _emailOptions.SSL);
            await client.AuthenticateAsync(_emailOptions.Email, _emailOptions.Pass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
