namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product rating class returned from the controller
    /// </summary>
    public class ProductRatingResponse
    {
        /// <summary>
        /// Rating of product
        /// </summary>
        /// <example>5</example>
        public float Rating { get; set; }
        /// <summary>
        /// Count of reviews
        /// </summary>
        /// <example>10</example>
        public int CountReviews { get; set; }
    }
}
