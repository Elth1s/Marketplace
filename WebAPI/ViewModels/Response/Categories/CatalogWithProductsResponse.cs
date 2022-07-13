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
    }
}
