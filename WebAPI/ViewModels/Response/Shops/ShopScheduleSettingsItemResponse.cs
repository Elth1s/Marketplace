namespace WebAPI.ViewModels.Response.Shops
{
    public class ShopScheduleSettingsItemResponse
    {
        /// <summary>
        /// Start time
        /// </summary>
        /// <example>9:00</example>
        public string Start { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        /// <example>18:00</example>
        public string End { get; set; }
        /// <summary>
        /// Is weekend
        /// </summary>
        /// <example>true</example>
        public bool IsWeekend { get; set; }
        /// <summary>
        /// Day of week identifier
        /// </summary>
        /// <example>1</example>
        public int DayOfWeekId { get; set; }
        /// <summary>
        /// Name of day
        /// </summary>
        /// <example>Monday</example>
        public string Name { get; set; }
    }
}
