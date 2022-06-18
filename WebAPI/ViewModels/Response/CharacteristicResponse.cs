namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Characteristic class returned from the controller
    /// </summary>
    public class CharacteristicResponse
    {
        /// <summary>
        /// Characteristic identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of characteristic
        /// </summary>
        /// <example>Package</example>
        public string Name { get; set; }
        /// <summary>
        /// Characteristic value
        /// </summary>
        /// <example>Documents</example>
        public string Value { get; set; }
        /// <summary>
        /// Name of characteristic group
        /// </summary>
        /// <example>Main</example>
        public string CharacteristicGroupName { get; set; }
    }
}
