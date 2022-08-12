namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Shop Info From Product class returned from the controller
    /// </summary>
    public class ShopInfoFromProductResponse
    {
        /// <summary>
        /// Name of shop
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Shop image
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Shop email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Shop URL
        /// </summary>
        public string SiteUrl { get; set; }
        /// <summary>
        /// Shop Adress
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// List of shop phones
        /// </summary>
        public IEnumerable<string> Phones { get; set; }
    }
}
