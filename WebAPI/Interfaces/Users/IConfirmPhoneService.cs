using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Interfaces.Users
{
    public interface IConfirmPhoneService
    {
        Task SendVerificationCodeAsync(string userId);

        Task VerificateAsync(string userId, ConfirmPhoneRequest request);

        Task<bool> IsPhoneConfirmed(string id);
    }
}
