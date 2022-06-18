namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Product status class returned from the controller
    /// </summary>
    public class ProductStatusResponse
    {
        /// <summary>
        /// Product status identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Product status name
        /// </summary>
        /// <example>In stock</example>
        public string Name { get; set; }
    }
}
