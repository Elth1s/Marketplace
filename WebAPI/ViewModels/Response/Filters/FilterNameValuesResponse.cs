namespace WebAPI.ViewModels.Response.Filters
{
    /// <summary>
    /// Filter name class returned from the controller
    /// </summary>
    public class FilterNameValuesResponse
    {
        /// <summary>
        /// Filer name identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of filter name
        /// </summary>
        /// <example>Brand name</example>
        public string Name { get; set; }
        /// <summary>
        /// Measure of unit
        /// </summary>
        /// <example>Main</example>
        public string UnitMeasure { get; set; }
        /// <summary>
        /// List of values
        /// </summary>
        public IEnumerable<FilterValueCatalogResponse> FilterValues { get; set; }
    }

    public class FilterValueCatalogResponse
    {
        /// <summary>
        /// Filer value identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of filter value
        /// </summary>
        /// <example>AMD</example>
        public string Value { get; set; }
    }
}
