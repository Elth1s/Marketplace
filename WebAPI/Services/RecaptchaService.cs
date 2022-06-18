using Microsoft.Extensions.Options;
using WebAPI.Interfaces;
using WebAPI.Settings;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly ReCaptchaSettings _reCaptchaSettings;
        public RecaptchaService(IOptions<ReCaptchaSettings> reCaptchaSettings)
        {
            _reCaptchaSettings = reCaptchaSettings.Value;
        }

        public bool IsValid(string recaptchaToken)
        {
            using var httpClient = new HttpClient();
            string requestComm = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                                               _reCaptchaSettings.SecretKey,
                                               recaptchaToken);
            var request = new HttpRequestMessage(HttpMethod.Get, requestComm);
            var response = httpClient.Send(request);
            using var reader = new StreamReader(response.Content.ReadAsStream());
            var GoogleReply = reader.ReadToEnd();

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaResponse>(GoogleReply);

            if (captchaResponse.Success)
            {
                return true;
            }
            else
            {
                return false;//throw new Exception("Виникла помилка при підтвердженні каптчи.");
            }
        }
    }
}
