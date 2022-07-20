namespace WebAPI.ViewModels.Response.Reviews
{
    /// <summary>
    /// Review image class returned from the controller
    /// </summary>
    public class ReviewImageResponse
    {
        /// <summary>
        /// Review image identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of review image
        /// </summary>
        /// <example>https://some_review_image_example.jpg</example>
        public string Name { get; set; }
    }
}
