namespace WebAPI.ViewModels.Request.Orders
{
    /// <summary>
    /// Order class to update order
    /// </summary>
    public class UpdateOrderRequest
    {
        /// <summary>
        /// Order status identifier
        /// </summary>
        /// <example>1</example>
        public int OrderStatusId { get; set; }
        /// <summary>
        /// Tracking Number
        /// </summary>
        /// <example>123</example>
        public string TrackingNumber { get; set; }
    }
}
