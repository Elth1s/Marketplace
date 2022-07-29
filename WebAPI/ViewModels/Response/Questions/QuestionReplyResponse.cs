namespace WebAPI.ViewModels.Response.Questions
{
    /// <summary>
    /// Question reply class returned from the controller
    /// </summary>
    public class QuestionReplyResponse
    {
        /// <summary>
        /// Question identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// User full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// Question reply date
        /// </summary>
        /// <example>20.07.2022</example>
        public string Date { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        /// <example>Some information</example>
        public string Text { get; set; }
        /// <summary>
        /// Is Seller reply
        /// </summary>
        /// <example>false</example>
        public bool IsSeller { get; set; }
    }
}
