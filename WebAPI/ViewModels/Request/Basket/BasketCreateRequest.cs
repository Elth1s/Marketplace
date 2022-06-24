namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Basket class to create basket 
    /// </summary>
    public class BasketCreateRequest
    {
        /// <summary>
        ///  Product identifier
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }

    }
}
