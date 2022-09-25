namespace WebAPI.ViewModels.Request.Products
{
    /// <summary>
    /// Product class to create product 
    /// </summary>
    public class ProductDiscountRequest
    {
        /// <summary>
        /// Discount of product
        /// </summary>
        /// <example>10</example>
        public int Discount { get; set; }
        /// <summary>
        /// Identifier of sale
        /// </summary>
        /// <example>10</example>
        public int? SaleId { get; set; }
    }
}
