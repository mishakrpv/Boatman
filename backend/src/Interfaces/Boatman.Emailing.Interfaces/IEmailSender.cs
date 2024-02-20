namespace Boatman.Emailing.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string toEmail, string subject, string content);
}