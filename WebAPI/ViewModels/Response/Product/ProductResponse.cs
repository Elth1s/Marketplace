namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Product class returned from the controller
    /// </summary>
    public class ProductResponse
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of product
        /// </summary>
        /// <example>T-shirt</example>
        public string Name { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Some description for product</example>
        public string Description { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        /// <example>1000</example>
        public float Price { get; set; }
        /// <summary>
        /// Product count
        /// </summary>
        /// <example>10</example>
        public int Count { get; set; }
        /// <summary>
        /// Product shop name
        /// </summary>
        /// <example>Smith's shop</example>
        public string ShopName { get; set; }
        /// <summary>
        /// Product status name
        /// </summary>
        /// <example>In stock</example>
        public string StatusName { get; set; }
        /// <summary>
        /// Product category name
        /// </summary>
        /// <example>Clothes</example>
        public string CategoryName { get; set; }
    }
}
