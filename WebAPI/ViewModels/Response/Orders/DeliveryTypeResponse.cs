namespace WebAPI.ViewModels.Response.Orders
{
    /// <summary>
    /// Delivery type class returned from the controller
    /// </summary>
    public class DeliveryTypeResponse
    {
        /// <summary>
        /// Delivery type identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Delivery type name
        /// </summary>
        /// <example>Active</example>
        public string Name { get; set; }
        /// <summary>
        /// Delivery type dark icon
        /// </summary>
        /// <example>https://some_delivery_type_dark_icon_example.jpg</example>
        public string DarkIcon { get; set; }
        /// <summary>
        /// Delivery type light icon
        /// </summary>
        /// <example>https://some_delivery_type_light_icon_example.jpg</example>
        public string LightIcon { get; set; }
    }
}
