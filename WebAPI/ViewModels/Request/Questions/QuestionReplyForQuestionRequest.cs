namespace WebAPI.ViewModels.Request.Questions
{
    /// <summary>
    /// Question reply for review class to pagination question reply
    /// </summary>
    public class QuestionReplyForQuestionRequest
    {
        /// <summary>
        /// Question identifier
        /// </summary>
        /// <example>1</example>
        public int QuestionId { get; set; }
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
