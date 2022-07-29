namespace WebAPI.ViewModels.Response.Characteristics
{
    /// <summary>
    /// Characteristic value class returned from the controller
    /// </summary>
    public class CharacteristicValueResponse
    {
        /// <summary>
        /// Characteristic value identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Value of characteristic value
        /// </summary>
        /// <example>Package</example>
        public string Value { get; set; }
        /// <summary>
        /// Identifier of characteristic name
        /// </summary>
        /// <example>1</example>
        public string CharacteristicNameId { get; set; }
        /// <summary>
        /// Name of characteristic name
        /// </summary>
        /// <example>Main</example>
        public string CharacteristicName { get; set; }
    }
}
