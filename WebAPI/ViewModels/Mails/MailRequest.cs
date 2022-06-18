namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Mail class for sending email
    /// </summary>
    public class MailRequest
    {
        /// <summary>
        /// Email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string ToEmail { get; set; }
        /// <summary>
        /// Letter subject
        /// </summary>
        /// <example>Confirm email</example>
        public string Subject { get; set; }
        /// <summary>
        /// Letter body
        /// </summary>
        /// <example>Confirm your email</example>
        public string Body { get; set; }
    }
}
