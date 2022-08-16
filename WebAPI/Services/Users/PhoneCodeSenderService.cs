using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Settings;
using WebAPI.ViewModels.Request;

namespace WebAPI.Services
{
    public class PhoneCodeSenderService : IPhoneCodeSenderService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly TwilioSettings _twilioSettings;
        public PhoneCodeSenderService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
            _errorMessagesLocalizer = errorMessagesLocalizer;
        }
        public async Task SendCodeAsync(PhoneRequest request)
        {

            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            var result = await VerificationResource.CreateAsync(
                to: request.Phone,
                channel: VerificationResource.ChannelEnum.Sms.ToString(),
                pathServiceSid: _twilioSettings.VerificationSid
            );
            if (result.Status == VerificationResource.StatusEnum.Canceled.ToString())
                throw new AppException(_errorMessagesLocalizer["CodeSendFailed"]);
        }

        public async Task VerifyCodeAsync(CodeRequest request)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            var result = await VerificationCheckResource.CreateAsync(
            to: request.Phone,
            code: request.Code,
            pathServiceSid: _twilioSettings.VerificationSid
            );

            if ((result.Valid.HasValue && !result.Valid.Value) || result.Status == VerificationResource.StatusEnum.Canceled.ToString())
                throw new AppException(_errorMessagesLocalizer["CodeValidationFailed"]);

        }
    }
}
