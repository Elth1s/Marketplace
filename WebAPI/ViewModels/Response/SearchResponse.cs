namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Search class returned from the controller
    /// </summary>
    public class SearchResponse<T> where T : class
    {
        /// <summary>
        /// Min price
        /// </summary>
        /// <example>100</example>
        public int Min { get; set; }
        /// <summary>
        /// Max price
        /// </summary>
        /// <example>10000</example>
        public int Max { get; set; }
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
