namespace WebAPI.ViewModels.Request.Reviews
{
    /// <summary>
    /// Review reply for review class to pagination review reply
    /// </summary>
    public class ReviewReplyForReviewRequest
    {
        /// <summary>
        /// Review identifier
        /// </summary>
        /// <example>1</example>
        public int ReviewId { get; set; }
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }

        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>8</example>
        public int RowsPerPage { get; set; }
    }
}
