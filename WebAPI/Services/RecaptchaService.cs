using WebAPI.Interfaces;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly IConfiguration _configuration;
        public RecaptchaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string recaptchaToken)
        {
            var client = new System.Net.WebClient();

            //TODO: Insert key in appsettings.json
            string PrivateKey = _configuration.GetValue<string>("Recaptcha:SecretKey");
            string requestComm = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, recaptchaToken);
            var GoogleReply = client.DownloadString(requestComm);

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
