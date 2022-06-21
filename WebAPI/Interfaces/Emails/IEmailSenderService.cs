using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces.Emails
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
