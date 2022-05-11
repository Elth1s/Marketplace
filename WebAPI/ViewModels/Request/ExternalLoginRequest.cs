namespace WebAPI.ViewModels.Request
{
    public class ExternalLoginRequest
    {
        public string Provider { get; set; }
        public string Token { get; set; }
    }
}
