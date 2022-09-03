namespace WebAPI.ViewModels.Response.Orders
{
    /// <summary>
    /// Order class returned from the controller
    /// </summary>
    public class OrderItemResponse
    {
        /// <summary>
        /// Shop name
        /// </summary>
        /// <example>Mall</example>
        public string ShopName { get; set; }
        /// <summary>
        /// Total price
        /// </summary>
        /// <example>13945</example>
        public float TotalPrice { get; set; }
        /// <summary>
        /// Total count
        /// </summary>
        /// <example>13</example>
        public float TotalCount { get; set; }
        /// <summary>
        /// List of products
        /// </summary>
        public IEnumerable<BasketOrderItemResponse> BasketItems { get; set; }

    }

    /// <summary>
    ///  Basket class returned from the controller
    /// </summary>
    public class BasketOrderItemResponse
    {
        /// <summary>
        /// Basket item identifier
        /// </summary>
        /// <example>2</example>
        public int Id { get; set; }
        /// <summary>
        /// Product identifier
        /// </summary>
        /// <example>2</example>
        public int ProductId { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        /// <example>HyperX Kingston SSD</example>
        public string ProductName { get; set; }
        /// <summary>
        /// Product image
        /// </summary>
        /// <example>2</example>
        public string ProductImage { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        /// <example>13945</example>
        public float ProductPrice { get; set; }
        /// <summary>
        /// Product url slug
        /// </summary>
        /// <example>13945</example>
        public string ProductUrlSlug { get; set; }
        /// <summary>
        /// Count of product in basket
        /// </summary>
        /// <example>2</example>
        public int Count { get; set; }
    }
}
