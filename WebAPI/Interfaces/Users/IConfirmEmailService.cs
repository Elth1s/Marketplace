using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Interfaces.Users
{
    public interface IConfirmEmailService
    {
        Task SendConfirmMailAsync(string userId);

        Task ConfirmEmailAsync(ConfirmEmailRequest request);

        Task<bool> IsEmailConfirmed(string id);
    }
}
