namespace WebAPI.ViewModels.Response.Filters
{
    /// <summary>
    /// Filter name class returned from the controller
    /// </summary>
    public class FilterNameResponse
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
        /// Filter group identifier
        /// </summary>
        /// <example>1</example>
        public string FilterGroupId { get; set; }
        /// <summary>
        /// Filter group name
        /// </summary>
        /// <example>Main</example>
        public string FilterGroupName { get; set; }
        /// <summary>
        /// Identifier of unit
        /// </summary>
        /// <example>1</example>
        public string UnitId { get; set; }
        /// <summary>
        /// Measure of unit
        /// </summary>
        /// <example>Main</example>
        public string UnitMeasure { get; set; }
    }
}
