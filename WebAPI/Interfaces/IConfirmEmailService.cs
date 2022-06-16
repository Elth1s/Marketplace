using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces
{
    public interface IConfirmEmailService
    {
        Task SendConfirmMailAsync(string userId);

        Task ConfirmEmailAsync(ConfirmEmailRequest request);

        Task<bool> IsEmailConfirmed(string id);
    }
}
