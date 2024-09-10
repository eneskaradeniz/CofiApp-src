using CofiApp.Contracts.Emails;

namespace CofiApp.Application.Abstractions.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
