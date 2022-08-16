namespace WebAPI.ViewModels.Response.Filters
{
    /// <summary>
    /// Filter group class returned from the controller
    /// </summary>
    public class FilterGroupFullInfoResponse
    {
        /// <summary>
        /// Characteristic group identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English name of the filter group
        /// </summary>
        /// <example>Main</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the filter group
        /// </summary>
        /// <example>Основна</example>
        public string UkrainianName { get; set; }
    }
}

