namespace WebAPI.ViewModels.Response.Order
{
    /// <summary>
    /// Order class returned from the controller
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        /// Order identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// ConsumerFirstName
        /// </summary>
        /// <example>"Novak"</example>
        public string ConsumerFirstName { get; set; }
        /// <summary>
        /// ConsumerSecondName
        /// </summary>
        /// <example>"Vova"</example>
        public string ConsumerSecondName { get; set; }
        /// <summary>
        /// ConsumerPhone
        /// </summary>
        /// <example>"+380962312161"</example>
        public string ConsumerPhone { get; set; }
        /// <summary>
        /// ConsumerEmail
        /// </summary>
        /// <example>"novak@gmail.com"</example>
        public string ConsumerEmail { get; set; }

        /// <summary>
        /// OrderStatusName
        /// </summary>
        /// <example>"Active"</example>
        public string OrderStatusName { get; set; }


        public IEnumerable<OrderProductResponse> OrderProductsResponse { get; set; }

    }

    public class OrderProductResponse
    {
        /// <summary>
        /// Order Product identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        /// <example>1000</example>
        public int Price { get; set; }

        /// <summary>
        /// OrderId
        /// </summary>
        /// <example>1</example>
        public int OrderId { get; set; }

        /// <summary>
        /// ProductId
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }
    }

}
