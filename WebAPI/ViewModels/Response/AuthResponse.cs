namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Class returned from the controller after user authorization
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// User access token
        /// </summary>
        /// <example>some_access_token</example>
        public string AccessToken { get; set; }
        /// <summary>
        /// User refresh token
        /// </summary>
        /// <example>some_refresh token</example>
        public string RefreshToken { get; set; }
    }
}
