namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product image class returned from the controller
    /// </summary>
    public class ProductImageResponse
    {
        /// <summary>
        /// Product image identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of product image
        /// </summary>
        /// <example>https://some_product_image_example.jpg</example>
        public string Name { get; set; }
        /// <summary>
        /// Product image priority
        /// </summary>
        /// <example>1</example>
        public int Priority { get; set; }
    }
}
