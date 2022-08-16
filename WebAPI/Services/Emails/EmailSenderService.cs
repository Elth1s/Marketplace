using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using WebAPI.Exceptions;
using WebAPI.Interfaces.Emails;
using WebAPI.Settings;
using WebAPI.ViewModels.Request;

namespace WebAPI.Services.Emails
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly MailSettings _mailSettings;
        public EmailSenderService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer, IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
            _errorMessagesLocalizer = errorMessagesLocalizer;
        }
        public async Task SendEmailAsync(MailRequest request)
        {
            var client = new MailjetClient(
                _mailSettings.ApiKey,
                _mailSettings.SecretKey);

            var response = await client.PostAsync(CreateMessage(request));
            if (!response.IsSuccessStatusCode)
                throw new AppException(_errorMessagesLocalizer["EmailSendingFailed"]);
        }
        private MailjetRequest CreateMessage(MailRequest request)
        {

            MailjetRequest mailjetRequest = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
             .Property(Send.Messages, new JArray {
            new JObject {
             {
              "From",
              new JObject {
               {"Email", _mailSettings.Email},
               {"Name", _mailSettings.DisplayName}
              }
             }, {
              "To",
              new JArray {
               new JObject {
                {
                 "Email",
                 request.ToEmail
                }
               }
              }
             }, {
              "Subject",
              request.Subject
             }, {
              "HTMLPart",
             request.Body
             }
            }
             });
            return mailjetRequest;
        }
    }
}
