using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace LoansManagementSystem.Services;

public class SmtpSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool UseSsl { get; set; }
}

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using (var client = new SmtpClient())
        {
            client.Connect(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSsl);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", _smtpSettings.Username));
            message.To.Add(new MailboxAddress("Recipient", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            await client.SendAsync(message);

            client.Disconnect(true);
        }
    }
}