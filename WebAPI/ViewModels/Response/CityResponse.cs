namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// City class returned from the controller
    /// </summary>
    public class CityResponse
    {
        /// <summary>
        /// City identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// City name
        /// </summary>
        /// <example>Atlanta</example>
        public string Name { get; set; }
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
