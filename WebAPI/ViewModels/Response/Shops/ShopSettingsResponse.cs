namespace WebAPI.ViewModels.Response.Shops
{
    /// <summary>
    /// Shop class returned from the controller
    /// </summary>
    public class ShopSettingsResponse
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
        /// Shop URL
        /// </summary>
        /// <example>https://some_shop_example_url.com</example>
        public string SiteUrl { get; set; }
        /// <summary>
        /// Seller full name
        /// </summary>
        /// <example>John Smith</example>
        public string UserFullName { get; set; }
        /// <summary>
        /// Shop email address
        /// </summary>
        /// <example>shop@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>1</example>
        public int? CountryId { get; set; }
        /// <summary>
        /// City identifier
        /// </summary>
        /// <example>2</example>
        public int? CityId { get; set; }

        public IEnumerable<ShopPhoneResponse> Phones { get; set; }
    }

    public class ShopPhoneResponse
    {
        public string Phone { get; set; }
        public string Comment { get; set; }

    }
}
