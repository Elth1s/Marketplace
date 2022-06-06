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
    public class ConfirmEmailService : IConfirmEmailService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSenderService _emailService;
        private readonly ITemplateService _templateService;
        private readonly ClientUrl _clientUrl;
        public ConfirmEmailService(UserManager<AppUser> userManager,
            IEmailSenderService emailSender,
            ITemplateService templateService,
            IOptions<ClientUrl> clientUrl)
        {
            _userManager = userManager;
            _emailService = emailSender;
            _templateService = templateService;
            _clientUrl = clientUrl.Value;
        }
        public async Task SendConfirmMailAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.UserNullChecking();
            user.UserEmailConfirmedChecking();


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl =
             $"{_clientUrl.ApplicationUrl}confirmEmail?userId={user.Id}&" +
             $"token={WebUtility.UrlEncode(token)}";

            await _emailService.SendEmailAsync(new MailRequest()
            {
                ToEmail = user.Email,
                Subject = "Mall confirm email",
                Body = await _templateService.GetTemplateHtmlAsStringAsync("Mails/ConfirmEmail",
                    new UserTokenRequest() { CallbackUrl = callbackUrl, Name = user.FirstName, Uri = _clientUrl.ApplicationUrl })
            });
        }

        public async Task ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            user.UserNullChecking();
            user.UserEmailConfirmedChecking();

            var result = await _userManager.ConfirmEmailAsync(user, request.ConfirmationCode);

            if (!result.Succeeded)
            {
                throw new AppException(ErrorMessages.InvalidConfirmToken);
            }

            await _userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<bool> IsEmailConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.UserNullChecking();

            return user.EmailConfirmed;
        }
    }
}
