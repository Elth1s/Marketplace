namespace WebAPI.ViewModels.Response.Characteristics
{
    /// <summary>
    /// Characteristic group class returned from the controller
    /// </summary>
    public class CharacteristicGroupResponse
    {
        /// <summary>
        /// Characteristic group identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of characteristic group
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
    }
}
