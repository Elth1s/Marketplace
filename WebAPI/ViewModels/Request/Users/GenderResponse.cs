namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Gender class returned from the controller
    /// </summary>
    public class GenderResponse
    {
        /// <summary>
        /// Gender identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Gender name
        /// </summary>
        /// <example>Man</example>
        public string Name { get; set; }
    }
}
