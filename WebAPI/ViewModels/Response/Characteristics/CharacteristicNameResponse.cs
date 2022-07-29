namespace WebAPI.ViewModels.Response.Characteristics
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
        /// Identifier of characteristic group
        /// </summary>
        /// <example>1</example>
        public string CharacteristicGroupId { get; set; }
        /// <summary>
        /// Name of characteristic group
        /// </summary>
        /// <example>Main</example>
        public string CharacteristicGroupName { get; set; }
        /// <summary>
        /// Identifier of unit
        /// </summary>
        /// <example>Main</example>
        public string UnitMeasure { get; set; }
        /// <summary>
        /// Measure of unit
        /// </summary>
        /// <example>1</example>
        public string UnitId { get; set; }
    }
}
