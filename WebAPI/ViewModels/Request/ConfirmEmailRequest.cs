namespace WebAPI.ViewModels.Request
{
    public class ConfirmEmailRequest
    {
        public string UserId { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
