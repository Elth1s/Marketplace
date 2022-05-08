namespace WebAPI.ViewModels.Request
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
