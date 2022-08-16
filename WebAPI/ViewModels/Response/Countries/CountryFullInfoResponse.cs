namespace WebAPI.ViewModels.Response.Countries
{
    /// <summary>
    /// Country class returned from the controller
    /// </summary>
    public class CountryFullInfoResponse
    {
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English name of country
        /// </summary>
        /// <example>Ukraine</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of country
        /// </summary>
        /// <example>Україна</example>
        public string UkrainianName { get; set; }
        /// <summary>
        /// Code of country
        /// </summary>
        /// <example>UA</example>
        public string Code { get; set; }
    }
}
