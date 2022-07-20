using Newtonsoft.Json;

namespace WebAPI.ViewModels.Response.Users
{
    /// <summary>
    /// Facebook class returned from the facebook response
    /// </summary>
    public class FacebookResponse
    {
        /// <summary>
        /// Facebook user identifier
        /// </summary>
        /// <example>123</example>
        public string Id { get; set; }
        /// <summary>
        /// Facebook user email
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Facebook user first name
        /// </summary>
        /// <example>Nick</example>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Facebook user second name
        /// </summary>
        /// <example>Smith</example>
        [JsonProperty("last_name")]
        public string SecondName { get; set; }
    }
}
