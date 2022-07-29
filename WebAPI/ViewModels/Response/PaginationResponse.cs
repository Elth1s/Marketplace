namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Pagination class returned from the controller
    /// </summary>
    public class PaginationResponse<T> where T : class
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
