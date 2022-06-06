namespace WebAPI.ViewModels.Request
{
    public class UserTokenRequest
    {
        public string CallbackUrl { get; set; }
        public string Name { get; set; }
        public Uri Uri { get; set; }
    }
}
