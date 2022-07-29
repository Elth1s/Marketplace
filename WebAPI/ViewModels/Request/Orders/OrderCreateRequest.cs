namespace WebAPI.ViewModels.Request.Orders
{
    /// <summary>
    /// Order class to create order
    /// </summary>
    public class OrderCreateRequest
    {
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
        /// Consumer Phone
        /// </summary>
        /// <example>email@gmail.com</example>
        public string ConsumerEmail { get; set; }

        public IEnumerable<OrderProductCreate> OrderProductsCreate { get; set; }
    }

    public class OrderProductCreate
    {
        /// <summary>
        ///  Count Order Product
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }
    }

}




