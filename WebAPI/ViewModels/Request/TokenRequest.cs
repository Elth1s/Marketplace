namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Class for user operations with refresh token
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Refresh token
        /// </summary>
        /// <example>some_refresh_token</example>
        public string Token { get; set; }
    }
}
