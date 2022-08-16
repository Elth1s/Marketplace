namespace WebAPI.ViewModels.Response.Filters
{
    /// <summary>
    /// Filter value class returned from the controller
    /// </summary>
    public class FilterValueFullInfoResponse
    {
        /// <summary>
        /// Filer value identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English value of the filter value
        /// </summary>
        /// <example>Discrete</example>
        public string EnglishValue { get; set; }
        /// <summary>
        /// Ukrainian value of the filter value
        /// </summary>
        /// <example>Дискретна</example>
        public string UkrainianValue { get; set; }
        /// <summary>
        /// Minimum for custom value
        /// </summary>
        /// <example>null</example>
        public int? Min { get; set; }
        /// <summary>
        /// Maximum for custom value
        /// </summary>
        /// <example>null</example>
        public int? Max { get; set; }
        /// <summary>
        /// Identifier of Filter name 
        /// </summary>
        /// <example>1</example>
        public string FilterNameId { get; set; }
        /// <summary>
        /// Name of Filter name 
        /// </summary>
        /// <example>Video card type</example>
        public string FilterName { get; set; }
    }
}
