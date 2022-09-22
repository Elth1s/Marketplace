namespace WebAPI.ViewModels.Response.Users
{
    /// <summary>
    /// User review class returned from the controller
    /// </summary>
    public class UserReviewResponse
    {
        /// <summary>
        /// Name of product
        /// </summary>
        /// <example>Hex Wednesday Dress</example>
        public string ProductName { get; set; }
        /// <summary>
        /// Product slug
        /// </summary>
        /// <example>some-product-slug</example>
        public string ProductSlug { get; set; }
        /// <summary>
        /// Product image
        /// </summary>
        /// <example>Hex Wednesday Dress</example>
        public string ProductImage { get; set; }
        /// <summary>
        /// Has user review
        /// </summary>
        /// <example>true</example>
        public bool HasReview { get; set; }
        /// <summary>
        /// Last user review
        /// </summary>
        /// <example>Some review</example>
        public string Review { get; set; }
    }
}
