namespace WebAPI.ViewModels.Response.Characteristics
{
    /// <summary>
    /// Characteristic group class returned from the controller
    /// </summary>
    public class CharacteristicGroupSellerResponse
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
        public IEnumerable<CharacteristicNameSellerResponse> CharacteristicNames { get; set; }
    }

    /// <summary>
    /// Characteristic name class returned from the controller
    /// </summary>
    public class CharacteristicNameSellerResponse
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
        public IEnumerable<CharacteristicValueSellerResponse> CharacteristicValues { get; set; }
    }

    /// <summary>
    /// Characteristic value class returned from the controller
    /// </summary>
    public class CharacteristicValueSellerResponse
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
    }
}
