using WebAPI.ViewModels.Response.Categories;

namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product class returned from the controller
    /// </summary>
    public class ProductWithCategoryParentsResponse
    {
        /// <summary>
        /// Product 
        /// </summary>
        public ProductPageResponse Product { get; set; }
        /// <summary>
        /// List of categories
        /// </summary>
        public IEnumerable<CatalogItemResponse> Parents { get; set; }
    }
}
