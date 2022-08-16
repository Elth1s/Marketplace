namespace WebAPI.ViewModels.Response.Orders
{
    /// <summary>
    /// Order status class returned from the controller
    /// </summary>
    public class OrderStatusFullInfoResponse
    {
        /// <summary>
        /// Order status identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English name of the order status
        /// </summary>
        /// <example>In Process</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of order status
        /// </summary>
        /// <example>В процесі</example>
        public string UkrainianName { get; set; }
    }

}
