using WebAPI.ViewModels.Response.Products;

namespace WebAPI.ViewModels.Response.Categories
{
    /// <summary>
    /// Category class returned from the controller
    /// </summary>
    public class CatalogWithProductsResponse
    {
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Computer equipment and software</example>
        public string Name { get; set; }
        /// <summary>
        /// List of categories
        /// </summary>
        public IEnumerable<CatalogItemResponse> CatalogItems { get; set; }
        /// <summary>
        /// List of products
        /// </summary>
        public IEnumerable<ProductCatalogResponse> Products { get; set; }
        /// <summary>
        /// Count of products
        /// </summary>
        /// <example>1</example>
        public int CountProducts { get; set; }
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
    }
}
