namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Shop class returned from the controller
    /// </summary>
    public class ShopResponse
    {
        /// <summary>
        /// Shop identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of shop
        /// </summary>
        /// <example>Smith's Shop</example>
        public string Name { get; set; }
        /// <summary>
        /// Shop description
        /// </summary>
        /// <example>Some shop description</example>
        public string Description { get; set; }
        /// <summary>
        /// Shop image
        /// </summary>
        /// <example>https://some_shop_image_example.jpg</example>
        public string Photo { get; set; }
        /// <summary>
        /// Shop email address
        /// </summary>
        /// <example>shop@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Shop URL
        /// </summary>
        /// <example>https://some_shop_example_url.com</example>
        public string SiteUrl { get; set; }
        /// <summary>
        /// Country name
        /// </summary>
        /// <example>USA</example>
        public string CountryName { get; set; }
        /// <summary>
        /// City name
        /// </summary>
        /// <example>Atlanta</example>
        public string CityName { get; set; }
    }
}
