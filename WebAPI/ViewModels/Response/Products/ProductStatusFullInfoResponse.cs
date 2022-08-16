namespace WebAPI.ViewModels.Response.Products
{
    /// <summary>
    /// Product status class returned from the controller
    /// </summary>
    public class ProductStatusFullInfoResponse
    {
        /// <summary>
        /// Product status identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// English name of the product status
        /// </summary>
        /// <example>In stock</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of product status
        /// </summary>
        /// <example>В наявності</example>
        public string UkrainianName { get; set; }
    }
}

