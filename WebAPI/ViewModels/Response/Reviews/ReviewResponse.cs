namespace WebAPI.ViewModels.Response.Reviews
{
    /// <summary>
    /// Review class returned from the controller
    /// </summary>
    public class ReviewResponse
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
        /// Product rating
        /// </summary>
        /// <example>4</example>
        public int ProductRating { get; set; }
        /// <summary>
        /// Review date
        /// </summary>
        /// <example>20.07.2022</example>
        public string Date { get; set; }
        /// <summary>
        /// Product advantage
        /// </summary>
        /// <example>Some list of benefits</example>
        public string Advantage { get; set; }
        /// <summary>
        /// Product disadvantage
        /// </summary>
        /// <example>Some list of disadvantages</example>
        public string Disadvantage { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        /// <example>Some information</example>
        public string Comment { get; set; }
        /// <summary>
        /// Video URL
        /// </summary>
        /// <example>https://some_video_url_example.jpg</example>
        public string VideoURL { get; set; }
        /// <summary>
        /// Is review liked
        /// </summary>
        /// <example>true</example>
        public bool IsLiked { get; set; }
        /// <summary>
        /// Is review disliked
        /// </summary>
        /// <example>false</example>
        public bool IsDisliked { get; set; }
        /// <summary>
        /// Review likes count
        /// </summary>
        /// <example>3</example>
        public int Dislikes { get; set; }
        /// <summary>
        /// Review dislikes count
        /// </summary>
        /// <example>2</example>
        public int Likes { get; set; }
        /// <summary>
        /// Review replies count
        /// </summary>
        /// <example>10</example>
        public int Replies { get; set; }
        /// <summary>
        /// List of review images
        /// </summary>
        public IEnumerable<string> Images { get; set; }

    }
}
