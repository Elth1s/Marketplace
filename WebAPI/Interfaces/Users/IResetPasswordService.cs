using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Interfaces.Users
{
    public interface IResetPasswordService
    {
        Task SendResetPasswordByEmailAsync(EmailRequest request);
        Task SendResetPasswordByPhoneAsync(PhoneRequest request);

        Task<UserTokenResponse> ValidatePhoneCodeAsync(CodeRequest request);

        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
