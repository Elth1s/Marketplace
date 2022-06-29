namespace WebAPI.ViewModels.Response.Countries
{
    /// <summary>
    /// Country class returned from the controller
    /// </summary>
    public class SearchCountryResponse
    {
        /// <summary>
        /// Count
        /// </summary>
        /// <example>190</example>
        public int Count { get; set; }
        /// <summary>
        /// List of country
        /// </summary>
        public IEnumerable<CountryResponse> Countries { get; set; }
    }
}
