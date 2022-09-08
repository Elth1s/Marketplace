namespace WebAPI.ViewModels.Request.Products
{
    /// <summary>
    /// Category class to get category with product
    /// </summary>
    public class NoveltyProductsRequest
    {
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }
        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>40</example>
        public int RowsPerPage { get; set; }
        /// <summary>
        /// List identifier of categories
        /// </summary>
        public IEnumerable<int> Categories { get; set; }
        /// <summary>
        /// List identifier of filters
        /// </summary>
        public IEnumerable<int> Filters { get; set; }
    }
}
