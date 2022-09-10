namespace WebAPI.ViewModels.Response.Cities
{
    /// <summary>
    /// City class returned from the controller
    /// </summary>
    public class CityForSelectResponse
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
    }
}
