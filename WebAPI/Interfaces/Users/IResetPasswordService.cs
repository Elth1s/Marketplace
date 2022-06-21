using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Interfaces.Users
{
    public interface IResetPasswordService
    {
        Task SendResetPasswordAsync(EmailRequest request);

        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
