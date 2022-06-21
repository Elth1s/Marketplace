namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Characteristic name class returned from the controller
    /// </summary>
    public class CharacteristicNameResponse
    {
        /// <summary>
        /// Characteristic name identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of characteristic name
        /// </summary>
        /// <example>Package</example>
        public string Name { get; set; }
        /// <summary>
        /// Name of characteristic group
        /// </summary>
        /// <example>Main</example>
        public string CharacteristicGroupName { get; set; }
        /// <summary>
        /// Measure of unit
        /// </summary>
        /// <example>Main</example>
        public string UnitMeasure { get; set; }
    }
}
