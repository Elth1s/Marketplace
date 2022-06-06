using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
