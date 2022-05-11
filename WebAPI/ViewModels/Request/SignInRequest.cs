namespace WebAPI.ViewModels.Request
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReCaptchaToken { get; set; }
    }
}
