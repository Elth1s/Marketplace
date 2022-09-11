namespace WebAPI.ViewModels.Response.Shops
{
    /// <summary>
    /// Shop page class returned from the controller
    /// </summary>
    public class ShopPageInfoResponse
    {
        /// <summary>
        /// Name of shop
        /// </summary>
        /// <example>Smith's Shop</example>
        public string Name { get; set; }
        /// <summary>
        /// Shop description
        /// </summary>
        /// <example>Some shop description</example>
        public string Description { get; set; }
        /// <summary>
        /// Average rating of service quality
        /// </summary>
        /// <example>4</example>
        public float AverageServiceQualityRating { get; set; }
        /// <summary>
        /// Average rating of timeliness
        /// </summary>
        /// <example>4</example>
        public float AverageTimelinessRating { get; set; }
        /// <summary>
        /// Average rating of information relevance
        /// </summary>
        /// <example>4</example>
        public float AverageInformationRelevanceRating { get; set; }
        /// <summary>
        /// Count of reviews
        /// </summary>
        /// <example>8</example>
        public int CountReviews { get; set; }
        /// <summary>
        /// Average rating
        /// </summary>
        /// <example>4.5</example>
        public float AverageRating { get; set; }
        /// <summary>
        /// List of ratings
        /// </summary>
        public IEnumerable<Rating> Ratings { get; set; }
        /// <summary>
        /// List of shop schedule items
        /// </summary>
        public IEnumerable<ShopScheduleItemResponse> Schedule { get; set; }
    }

    public class ShopScheduleItemResponse
    {
        /// <summary>
        /// Start time
        /// </summary>
        /// <example>9:00</example>
        public string Start { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        /// <example>18:00</example>
        public string End { get; set; }
        /// <summary>
        /// Is weekend
        /// </summary>
        /// <example>true</example>
        public bool IsWeekend { get; set; }
        /// <summary>
        /// Short name
        /// </summary>
        /// <example>Mo-Th</example>
        public string ShortNames { get; set; }
    }

    public class Rating
    {
        /// <summary>
        /// Number of rating
        /// </summary>
        /// <example>5</example>
        public int Number { get; set; }
        /// <summary>
        /// Count of rating
        /// </summary>
        /// <example>8</example>
        public int Count { get; set; }
    }
}
