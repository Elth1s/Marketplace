namespace WebAPI.ViewModels.Response.Sales
{
    /// <summary>
    /// Sale class returned from the controller
    /// </summary>
    public class SaleFullInfoResponse
    {
        /// <summary>
        /// Sale identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of sale
        /// </summary>
        /// <example>Sale Clothes</example>
        public string Name { get; set; }

        /// <summary>
        /// Ukrainian horizontal image of sale
        /// </summary>
        /// <example>https://some_horizontal_image_example.jpg</example>
        public string UkrainianHorizontalImage { get; set; }
        /// <summary>
        /// Ukrainian vertical image of sale
        /// </summary>
        /// <example>https://some_vertical_image_example.jpg</example>
        public string UkrainianVerticalImage { get; set; }
        /// <summary>
        /// English horizontal image of sale
        /// </summary>
        /// <example>https://some_horizontal_image_example.jpg</example>
        public string EnglishHorizontalImage { get; set; }
        /// <summary>
        /// English vertical image of sale
        /// </summary>
        /// <example>https://some_vertical_image_example.jpg</example>
        public string EnglishVerticalImage { get; set; }

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
        /// Date start of sale
        /// </summary>
        /// <example>12.09.2022</example>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Date end of sale
        /// </summary>
        /// <example>12.10.2022</example>
        public DateTime DateEnd { get; set; }


    }
}
