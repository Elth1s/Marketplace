using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Resources;
using WebAPI.Settings;
using WebAPI.ViewModels.Request;

namespace WebAPI.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSenderService _emailService;
        private readonly ITemplateService _templateService;
        private readonly ClientUrl _clientUrl;

        public ResetPasswordService(UserManager<AppUser> userManager,
            IEmailSenderService emailSender,
            ITemplateService templateService,
            IOptions<ClientUrl> clientUrl)
        {
            _userManager = userManager;
            _emailService = emailSender;
            _templateService = templateService;
            _clientUrl = clientUrl.Value;
        }

        public async Task SendResetPasswordAsync(EmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            user.UserNullChecking();


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl =
             $"{_clientUrl.ApplicationUrl}resetPassword/{WebUtility.UrlEncode(token)}?userId={user.Id}";

            await _emailService.SendEmailAsync(new MailRequest()
            {
                ToEmail = user.Email,
                Subject = "Mall reset password",
                Body = await _templateService.GetTemplateHtmlAsStringAsync("Mails/ResetPassword",
                    new UserTokenRequest() { CallbackUrl = callbackUrl, Name = user.FirstName, Uri = _clientUrl.ApplicationUrl })
            });
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            user.UserNullChecking();

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                throw new AppException(ErrorMessages.InvalidResetPasswordToken);
            }
        }

    }
}
