namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Comparison class returned from the controller
    /// </summary>
    public class ComparisonResponse
    {
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Computer equipment and software</example>
        public string CategoryName { get; set; }

        /// <summary>
        /// List of products
        /// </summary>
        public IEnumerable<ComparisonProduct> Products { get; set; }
        /// <summary>
        /// List of shops
        /// </summary>
        public IEnumerable<ComparisonShop> Shops { get; set; }
        /// <summary>
        /// List of filters
        /// </summary>
        public IEnumerable<ComparisonFilter> Filters { get; set; }
    }

    public class ComparisonProduct
    {
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Image { get; set; }
    }

    public class ComparisonShop
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
    }

    public class ComparisonFilter
    {
        public string FilterName { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}
