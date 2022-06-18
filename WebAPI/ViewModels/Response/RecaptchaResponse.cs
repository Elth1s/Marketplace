using Newtonsoft.Json;

namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// ReCaptcha class returned from the reCaptcha response
    /// </summary>
    public class RecaptchaResponse
    {
        /// <summary>
        /// Is reCaptcha verification successful
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Error messages if verification failed
        /// </summary>
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
