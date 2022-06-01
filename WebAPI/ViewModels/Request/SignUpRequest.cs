namespace WebAPI.ViewModels.Request
{
    public class SignUpRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReCaptchaToken { get; set; }
    }
}
