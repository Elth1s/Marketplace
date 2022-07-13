namespace WebAPI.ViewModels.Request.Categories
{
    /// <summary>
    /// Category class to create and update category
    /// </summary>
    public class CatalogWithProductsRequest
    {
        /// <summary>
        /// Url of category
        /// </summary>
        /// <example>technology-and-electronics</example>
        public string UrlSlug { get; set; }
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
        /// List identifier of filters
        /// </summary>
        public IEnumerable<int> Filters { get; set; }
    }
}
