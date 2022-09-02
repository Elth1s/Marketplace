namespace WebAPI.ViewModels.Response.Categories
{
    /// <summary>
    /// Category class returned from the controller
    /// </summary>
    public class CategoryResponse
    {
        /// <summary>
        /// Category identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
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
        /// Category image
        /// </summary>
        /// <example>https://some_category_image_example.jpg</example>
        public string Image { get; set; }
        /// <summary>
        /// Category light icon
        /// </summary>
        /// <example>https://some_category_light_icon_example.jpg</example>
        public string LightIcon { get; set; }
        /// <summary>
        /// Category dark icon
        /// </summary>
        /// <example>https://some_category_dark_icon_example.jpg</example>
        public string DarkIcon { get; set; }
        /// <summary>
        /// Category active icon
        /// </summary>
        /// <example>https://some_category_active_icon_example.jpg</example>
        public string ActiveIcon { get; set; }
        /// <summary>
        /// Parent category identifier
        /// </summary>
        /// <example>1</example>
        public string ParentId { get; set; }
        /// <summary>
        /// Parent category name
        /// </summary>
        /// <example>Technology and electronics</example>
        public string ParentName { get; set; }
    }
}
