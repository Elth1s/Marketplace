using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Interfaces.Users;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Services.Users
{
    public class ConfirmPhoneService : IConfirmPhoneService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhoneCodeSenderService _phoneService;
        public ConfirmPhoneService(UserManager<AppUser> userManager, IPhoneCodeSenderService phoneService)
        {
            _userManager = userManager;
            _phoneService = phoneService;
        }

        public async Task SendVerificationCodeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.UserNullChecking();
            user.UserPhoneConfirmedChecking();

            await _phoneService.SendCodeAsync(new PhoneRequest() { Phone = user.PhoneNumber });
        }

        public async Task VerificateAsync(string userId, ConfirmPhoneRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.UserNullChecking();
            user.UserPhoneConfirmedChecking();

            await _phoneService.VerifyCodeAsync(new CodeRequest()
            {
                Phone = user.PhoneNumber,
                Code = request.Code
            });
            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
        }
        public async Task<bool> IsPhoneConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            return user.PhoneNumberConfirmed;
        }
    }
}
