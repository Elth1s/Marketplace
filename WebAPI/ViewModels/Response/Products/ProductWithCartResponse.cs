namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product with cart class returned from the controller
    /// </summary>
    public class ProductWithCartResponse : ProductCatalogResponse
    {
        /// <summary>
        /// Is product in cart
        /// </summary>
        /// <example>true</example>
        public bool IsInBasket { get; set; }
    }
}
