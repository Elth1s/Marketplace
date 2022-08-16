namespace WebAPI.ViewModels.Response.Cities
{
    /// <summary>
    /// City class returned from the controller
    /// </summary>
    public class CityFullInfoResponse
    {
        /// <summary>
        /// City identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English name of city
        /// </summary>
        /// <example>Atlanta</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of city
        /// </summary>
        /// <example>Атланта</example>
        public string UkrainianName { get; set; }
        /// <summary>
        /// Country id
        /// </summary>
        /// <example>12</example>
        public int CountryId { get; set; }
        /// <summary>
        /// Country name
        /// </summary>
        /// <example>USA</example>
        public string CountryName { get; set; }
    }
}
