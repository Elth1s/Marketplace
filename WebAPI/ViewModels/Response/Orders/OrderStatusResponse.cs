namespace WebAPI.ViewModels.Response.Orders
{
    /// <summary>
    /// Order status class returned from the controller
    /// </summary>
    public class OrderStatusResponse
    {
        /// <summary>
        /// Order status identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Order status name
        /// </summary>
        /// <example>Active</example>
        public string Name { get; set; }

    }
}
