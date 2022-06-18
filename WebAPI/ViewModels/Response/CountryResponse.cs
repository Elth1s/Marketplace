namespace WebAPI.ViewModels.Response
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
        /// <example>USA</example>
        public string Name { get; set; }
    }
}
