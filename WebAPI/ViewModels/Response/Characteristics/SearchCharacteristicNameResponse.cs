namespace WebAPI.ViewModels.Response.Characteristics
{
    public class SearchCharacteristicNameResponse
    {
        /// <summary>
        /// Count
        /// </summary>
        /// <example>190</example>
        public int Count { get; set; }
        /// <summary>
        /// List of characteristic name
        /// </summary>
        public IEnumerable<CharacteristicNameResponse> CharacteristicNames { get; set; }
    }
}
