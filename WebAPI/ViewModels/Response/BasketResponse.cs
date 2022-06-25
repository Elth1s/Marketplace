namespace WebAPI.ViewModels.Response
{
    /// <summary>
    ///  Basket class returned from the controller
    /// </summary>
    public class BasketResponse
    {
        /// <summary>
        /// Basket identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        ///  Name
        /// </summary>
        /// <example>T-shirt</example>
        public string ProductName { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        /// <example>https://some_product_image_example.jpg</example>
        public string ProductImage { get; set; }
        /// <summary>
        ///  Price
        /// </summary>
        /// <example>1000</example>
        public float ProductPrice { get; set; }
        /// <summary>
        ///  Count
        /// </summary>
        /// <example>1000</example>
        public int ProductCount { get; set; }

    }
}
