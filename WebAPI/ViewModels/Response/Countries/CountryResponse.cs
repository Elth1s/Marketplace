namespace WebAPI.ViewModels.Response.Countries
{
    /// <summary>
    /// Country class returned from the controller
    /// </summary>
    public class CountryResponse
    {
        /// <summary>
        /// Country identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of country
        /// </summary>
        /// <example>Ukraine</example>
        public string Name { get; set; }
        /// <summary>
        /// Code of country
        /// </summary>
        /// <example>UA</example>
        public string Code { get; set; }
    }
}
