namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question for product class to pagination question
    /// </summary>
    public class QuestionForProductRequest
    {
        /// <summary>
        /// Product slug
        /// </summary>
        /// <example>some-product-slug</example>
        public string ProductSlug { get; set; }
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
