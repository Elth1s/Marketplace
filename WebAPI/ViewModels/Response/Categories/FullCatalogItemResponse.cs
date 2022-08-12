namespace WebAPI.ViewModels.Response.Categories
{
    /// <summary>
    /// Category class returned from the controller
    /// </summary>
    public class FullCatalogItemResponse
    {
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Computer equipment and software</example>
        public string Name { get; set; }
        /// <summary>
        /// Url of category
        /// </summary>
        /// <example>technology-and-electronics</example>
        public string UrlSlug { get; set; }
        /// <summary>
        /// Category icon
        /// </summary>
        /// <example>https://some_category_icon_example.jpg</example>
        public string Icon { get; set; }
        /// <summary>
        /// List of category childs
        /// </summary>
        public IEnumerable<FullCatalogItemResponse> Children { get; set; }
    }
}
