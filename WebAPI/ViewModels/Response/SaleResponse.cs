namespace WebAPI.ViewModels.Response
{
    public class SaleResponse
    {
        /// <summary>
        /// Name of sale
        /// </summary>
        /// <example>Sale Clothes</example>
        public string Name { get; set; }

        /// <summary>
        /// Image of sale
        /// </summary>
        /// <example>""</example>
        public string Image { get; set; }

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
