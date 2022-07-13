namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product class returned from the controller
    /// </summary>
    public class ProductCatalogResponse
    {
        /// <summary>
        /// Name of product
        /// </summary>
        /// <example>Hex Wednesday Dress</example>
        public string Name { get; set; }
        /// <summary>
        /// Photo of product
        /// </summary>
        /// <example>https://some_shop_image_example.jpg</example>
        public string Image { get; set; }
        /// <summary>
        /// Product status
        /// </summary>
        /// <example>In stock</example>
        public string StatusName { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        /// <example>1000</example>
        public float Price { get; set; }
        /// <summary>
        /// Discount
        /// </summary>
        /// <example>10</example>
        public int Discount { get; set; }
        /// <summary>
        /// Url slug
        /// </summary>
        /// <example>hex-wednesday-dress</example>
        public string UrlSlug { get; set; }
    }
}
