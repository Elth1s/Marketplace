namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Filter value class returned from the controller
    /// </summary>
    public class FilterValueResponse
    {
        /// <summary>
        /// Filer value identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Value of filter value
        /// </summary>
        /// <example>null</example>
        public string Value { get; set; }
        /// <summary>
        /// Minimum for custom value
        /// </summary>
        /// <example>1</example>
        public int? Min { get; set; }
        /// <summary>
        /// Maximum for custom value
        /// </summary>
        /// <example>1000</example>
        public int? Max { get; set; }
        /// <summary>
        /// Name of Filter name 
        /// </summary>
        /// <example>1</example>
        public string FilterName { get; set; }
    }
}
