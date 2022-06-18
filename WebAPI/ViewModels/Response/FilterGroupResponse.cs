namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Filter group class returned from the controller
    /// </summary>
    public class FilterGroupResponse
    {
        /// <summary>
        /// Characteristic group identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of filter group
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
    }
}
