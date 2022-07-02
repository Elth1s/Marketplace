namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Search class returned from the controller
    /// </summary>
    public class AdminSearchResponse<T> where T : class
    {
        /// <summary>
        /// Count
        /// </summary>
        /// <example>190</example>
        public int Count { get; set; }
        /// <summary>
        /// List of values
        /// </summary>
        public IEnumerable<T> Values { get; set; }
    }
}
