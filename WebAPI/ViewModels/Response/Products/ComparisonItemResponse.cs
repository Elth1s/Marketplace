namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Comparison class returned from the controller
    /// </summary>
    public class ComparisonItemResponse
    {
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Computer equipment and software</example>
        public string CategoryName { get; set; }
        /// <summary>
        /// Category slug
        /// </summary>
        /// <example>some-category-slug</example>
        public string UrlSlug { get; set; }
        /// <summary>
        /// Count of product
        /// </summary>
        /// <example>3</example>
        public int Count { get; set; }
    }
}
