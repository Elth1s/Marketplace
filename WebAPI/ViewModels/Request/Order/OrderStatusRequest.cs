namespace WebAPI.ViewModels.Request.Order
{
    public class OrderStatusRequest
    {
        /// <summary>
        /// Name of order status
        /// </summary>
        /// <example>"Active"</example>
        public string Name { get; set; }
    }
}
