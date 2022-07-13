namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Basket class to create basket 
    /// </summary>
    public class BasketCreateRequest
    {
        /// <summary>
        /// Product url slug
        /// </summary>
        /// <example>qweqdqdq-qweqqdqd-qweq</example>
        public string UrlSlug { get; set; }

    }
}
