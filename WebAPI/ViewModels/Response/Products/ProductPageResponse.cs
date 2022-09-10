namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product class returned from the controller
    /// </summary>
    public class ProductPageResponse
    {
        /// <summary>
        /// Is product in basket
        /// </summary>
        /// <example>false</example>
        public bool IsInBasket { get; set; }
        /// <summary>
        /// Is product selected
        /// </summary>
        /// <example>true</example>
        public bool IsSelected { get; set; }
        /// <summary>
        /// Name of product
        /// </summary>
        /// <example>T-shirt</example>
        public string Name { get; set; }
        /// <summary>
        /// Id of product shop
        /// </summary>
        /// <example>Mall</example>
        public int ShopId { get; set; }
        /// <summary>
        /// Name of product shop
        /// </summary>
        /// <example>Mall</example>
        public string ShopName { get; set; }
        /// <summary>
        /// Status of product
        /// </summary>
        /// <example>Mall</example>
        public string ProductStatus { get; set; }
        /// <summary>
        /// Rating of product shop
        /// </summary>
        /// <example>3</example>
        public float ShopRating { get; set; }
        /// <summary>
        /// List of product images
        /// </summary>
        public IEnumerable<ProductImageResponse> Images { get; set; }
        /// <summary>
        /// Price of product
        /// </summary>
        /// <example>1200</example>
        public float Price { get; set; }
        /// <summary>
        /// Product discount
        /// </summary>
        /// <example>1000</example>
        public float? Discount { get; set; }
        /// <summary>
        /// List of product filters
        /// </summary>
        public IEnumerable<ProductFilterValue> Filters { get; set; }
    }

    public class ProductFilterValue
    {
        public string Value { get; set; }
        public string FilterName { get; set; }
        public string UnitMeasure { get; set; }
    }
}
