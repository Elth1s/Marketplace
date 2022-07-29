namespace WebAPI.ViewModels.Response.Questions
{
    /// <summary>
    /// Question class returned from the controller
    /// </summary>
    public class QuestionResponse
    {
        /// <summary>
        /// Review identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// Review date
        /// </summary>
        /// <example>20.07.2022</example>
        public string Date { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        /// <example>Some information</example>
        public string Message { get; set; }
        /// <summary>
        /// Is question liked
        /// </summary>
        /// <example>true</example>
        public bool IsLiked { get; set; }
        /// <summary>
        /// Is question disliked
        /// </summary>
        /// <example>false</example>
        public bool IsDisliked { get; set; }
        /// <summary>
        /// Question likes count
        /// </summary>
        /// <example>3</example>
        public int Dislikes { get; set; }
        /// <summary>
        /// Question dislikes count
        /// </summary>
        /// <example>2</example>
        public int Likes { get; set; }
        /// <summary>
        /// Question replies count
        /// </summary>
        /// <example>10</example>
        public int Replies { get; set; }
        /// <summary>
        /// List of question images
        /// </summary>
        public IEnumerable<string> Images { get; set; }

    }
}
