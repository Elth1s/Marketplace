using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces
{
    public interface IResetPasswordService
    {
        Task SendResetPasswordAsync(EmailRequest request);

        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
