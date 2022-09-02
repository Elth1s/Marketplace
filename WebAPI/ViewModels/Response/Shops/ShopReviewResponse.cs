namespace WebAPI.ViewModels.Response.Shops
{
    /// <summary>
    /// Review class returned from the controller
    /// </summary>
    public class ShopReviewResponse
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
        ///  Service quality rating
        /// </summary>
        /// <example>4</example>
        public int ServiceQualityRating { get; set; }
        /// <summary>
        /// Timeliness rating
        /// </summary>
        /// <example>4</example>
        public int TimelinessRating { get; set; }
        /// <summary>
        /// Information relevance rating
        /// </summary>
        /// <example>4</example>
        public int InformationRelevanceRating { get; set; }
        /// <summary>
        /// Review date
        /// </summary>
        /// <example>20.07.2022</example>
        public string Date { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        /// <example>Some information</example>
        public string Comment { get; set; }
    }
}
