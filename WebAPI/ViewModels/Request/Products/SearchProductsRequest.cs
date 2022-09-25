namespace WebAPI.ViewModels.Request.Products
{
    /// <summary>
    /// Category class to get category with product
    /// </summary>
    public class SearchProductsRequest
    {
        /// <summary>
        /// Name of product
        /// </summary>
        /// <example>HyperX SSD</example>
        public string ProductName { get; set; }
        /// <summary>
        /// Shop identifier
        /// </summary>
        /// <example>1</example>
        public int? ShopId { get; set; }
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
        /// Min price
        /// </summary>
        /// <example>100</example>
        public int? Min { get; set; }
        /// <summary>
        /// Max price
        /// </summary>
        /// <example>10000</example>
        public int? Max { get; set; }
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
