namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Sale class returned from the controller
    /// </summary>
    public class SaleResponse
    {
        /// <summary>
        /// Name of sale
        /// </summary>
        /// <example>Sale Clothes</example>
        public string Name { get; set; }

        /// <summary>
        /// Horizontal image of sale
        /// </summary>
        /// <example>https://some_horizontal_image_example.jpg</example>
        public string HorizontalImage { get; set; }
        /// <summary>
        /// Vertical image of sale
        /// </summary>
        /// <example>https://some_vertical_image_example.jpg</example>
        public string VerticalImage { get; set; }

        /// <summary>
        /// Discount Min of sale
        /// </summary>
        /// <example>1</example>
        public int DiscountMin { get; set; }
        /// <summary>
        /// Discount Max of sale
        /// </summary>
        /// <example>99</example>
        public int DiscountMax { get; set; }

        /// <summary>
        /// Date Start of sale
        /// </summary>
        /// <example>12.09.2022</example>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Date End of sale
        /// </summary>
        /// <example>12.10.2022</example>
        public DateTime DateEnd { get; set; }
    }
}
