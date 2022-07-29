namespace WebAPI.ViewModels.Response.Orders
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
        /// Consumer First Name
        /// </summary>
        /// <example>Nick</example>
        public string ConsumerFirstName { get; set; }
        /// <summary>
        /// Consumer Second Name
        /// </summary>
        /// <example>Smith</example>
        public string ConsumerSecondName { get; set; }
        /// <summary>
        /// Consumer Phone
        /// </summary>
        /// <example>+380 50 638 8216</example>
        public string ConsumerPhone { get; set; }
        /// <summary>
        /// Consumer Email
        /// </summary>
        /// <example>email@gmail.com</example>
        public string ConsumerEmail { get; set; }

        /// <summary>
        /// Order Status Name
        /// </summary>
        /// <example>"In Process</example>
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
        public float Price { get; set; }

        /// <summary>
        /// ProductId
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        /// <example>T-shirt</example>
        public string ProductName { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        /// <example>https://some_product_image_example.jpg</example>
        public string ProductImage { get; set; }
    }

}
