namespace WebAPI.ViewModels.Request
{

    /// <summary>
    /// User token class for sending email to user with callback
    /// </summary>
    public class UserTokenRequest
    {
        /// <summary>
        /// Email callback URL
        /// </summary>
        /// <example>https://some_callback_example_url.com/some_page</example>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        /// <example>Nick</example>
        public string Name { get; set; }
        /// <summary>
        /// Site URL
        /// </summary>
        /// <example>https://some_example_url.com</example>
        public Uri Uri { get; set; }
    }
}
