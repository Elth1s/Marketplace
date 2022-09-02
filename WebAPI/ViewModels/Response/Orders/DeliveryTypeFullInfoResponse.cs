namespace WebAPI.ViewModels.Response.Orders
{
    /// <summary>
    /// Delivery type class returned from the controller
    /// </summary>
    public class DeliveryTypeFullInfoResponse
    {
        /// <summary>
        /// Delivery type identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English name of the delivery type
        /// </summary>
        /// <example>In Process</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of delivery type
        /// </summary>
        /// <example>В процесі</example>
        public string UkrainianName { get; set; }
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
