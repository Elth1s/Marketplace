namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Unit class returned from the controller
    /// </summary>
    public class UnitResponse
    {
        /// <summary>
        /// Unit identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Unit measure
        /// </summary>
        /// <example>m</example>
        public string Measure { get; set; }
    }
}
