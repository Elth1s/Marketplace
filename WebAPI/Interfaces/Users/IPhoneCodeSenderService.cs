using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces.Users
{
    public interface IPhoneCodeSenderService
    {
        Task SendCodeAsync(PhoneRequest request);

        Task VerifyCodeAsync(CodeRequest request);
    }
}
