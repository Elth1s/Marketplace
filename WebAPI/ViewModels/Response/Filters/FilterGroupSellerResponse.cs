namespace WebAPI.ViewModels.Response.Filters
{
    public class FilterGroupSellerResponse
    {
        /// <summary>
        /// Filer group identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of filter group
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
        /// <summary>
        /// List of names
        /// </summary>
        public IEnumerable<FilterNameSellerResponse> FilterNames { get; set; }
    }

    /// <summary>
    /// Filter name class returned from the controller
    /// </summary>
    public class FilterNameSellerResponse
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
        public IEnumerable<FilterValueSellerResponse> FilterValues { get; set; }
    }

    public class FilterValueSellerResponse
    {
        /// <summary>
        /// Filer value identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of filter value
        /// </summary>
        /// <example>Weight</example>
        public string Value { get; set; }
        /// <summary>
        /// Min of filter value
        /// </summary>
        /// <example>10</example>
        public int Min { get; set; }
        /// <summary>
        /// Max of filter value
        /// </summary>
        /// <example>100</example>
        public int Max { get; set; }
    }
}
