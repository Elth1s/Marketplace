namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// User token class returned from the controller
    /// </summary>
    public class UserTokenResponse
    {
        /// <summary>
        /// User identifier
        /// </summary>
        /// <example>94ddd073</example>
        public string UserId { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        /// <example>some_token</example>
        public string Token { get; set; }
    }
}
