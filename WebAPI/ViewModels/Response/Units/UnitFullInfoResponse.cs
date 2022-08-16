namespace WebAPI.ViewModels.Response.Units
{
    /// <summary>
    /// Unit class returned from the controller
    /// </summary>
    public class UnitFullInfoResponse
    {
        /// <summary>
        /// Unit identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English unit measure
        /// </summary>
        /// <example>m</example>
        public string EnglishMeasure { get; set; }
        /// <summary>
        /// Ukrainian unit measure
        /// </summary>
        /// <example>м</example>
        public string UkrainianMeasure { get; set; }
    }
}
