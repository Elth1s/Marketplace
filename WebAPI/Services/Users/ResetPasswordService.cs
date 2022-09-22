using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Net;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Emails;
using WebAPI.Interfaces.Users;
using WebAPI.Settings;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Services.Users
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IStringLocalizer<MailResources> _mailResourcesLocalizer;
        private readonly IEmailSenderService _emailService;
        private readonly ITemplateService _templateService;
        private readonly IPhoneCodeSenderService _phoneCodeSenderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly PhoneNumberManager _phoneNumberManager;
        private readonly ClientUrl _clientUrl;

        public ResetPasswordService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
                                    IStringLocalizer<MailResources> mailResourcesLocalizer,
                                    IEmailSenderService emailSender,
                                    ITemplateService templateService,
                                    IPhoneCodeSenderService phoneCodeSenderService,
                                    UserManager<AppUser> userManager,
                                    PhoneNumberManager phoneNumberManager,
                                    IOptions<ClientUrl> clientUrl)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _mailResourcesLocalizer = mailResourcesLocalizer;
            _userManager = userManager;
            _emailService = emailSender;
            _templateService = templateService;
            _phoneNumberManager = phoneNumberManager;
            _phoneCodeSenderService = phoneCodeSenderService;
            _clientUrl = clientUrl.Value;
        }

        public async Task SendResetPasswordByEmailAsync(EmailRequest request)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);

            user.UserNullChecking();


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl =
             $"{_clientUrl.ApplicationUrl}resetPassword/{WebUtility.UrlEncode(token)}?userId={user.Id}";

            await _emailService.SendEmailAsync(new MailRequest()
            {
                ToEmail = user.Email,
                Subject = _mailResourcesLocalizer["EmailThemeResetPassword"],
                Body = await _templateService.GetTemplateHtmlAsStringAsync("Mails/ResetPassword",
                    new UserTokenRequest() { CallbackUrl = callbackUrl, Name = user.FirstName, Uri = _clientUrl.ApplicationUrl })
            });
        }

        public async Task SendResetPasswordByPhoneAsync(PhoneRequest request)
        {
            request.Phone = _phoneNumberManager.GetPhoneE164Format(request.Phone);

            var user = await _userManager.GetByPhoneNumberAsync(request.Phone);

            user.UserNullChecking();

            await _phoneCodeSenderService.SendCodeAsync(request);
        }

        public async Task<UserTokenResponse> ValidatePhoneCodeAsync(CodeRequest request)
        {
            request.Phone = _phoneNumberManager.GetPhoneE164Format(request.Phone);

            var user = await _userManager.GetByPhoneNumberAsync(request.Phone);

            user.UserNullChecking();

            await _phoneCodeSenderService.VerifyCodeAsync(request);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var response = new UserTokenResponse()
            {
                UserId = user.Id,
                Token = WebUtility.UrlEncode(token)
            };

            return response;
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            user.UserNullChecking();

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
                throw new AppException(_errorMessagesLocalizer["InvalidResetPasswordToken"]);
        }


    }
}
