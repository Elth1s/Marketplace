namespace WebAPI.ViewModels.Response.Characteristics
{
    /// <summary>
    /// Characteristic groups class returned from the controller
    /// </summary>
    public class SearchCharacteristicGroupResponse
    {
        /// <summary>
        /// Count
        /// </summary>
        /// <example>190</example>
        public int Count { get; set; }
        /// <summary>
        /// List of characteristic groups
        /// </summary>
        public IEnumerable<CharacteristicGroupResponse> CharacteristicGroups { get; set; }
    }
}
