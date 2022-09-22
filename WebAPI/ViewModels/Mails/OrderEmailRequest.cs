namespace WebAPI.ViewModels.Mails
{
    /// <summary>
    /// Order email class for sending email to user with order
    /// </summary>
    public class OrderEmailRequest
    {
        /// <summary>
        /// Order identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        /// <example>Nick</example>
        public string Name { get; set; }
        /// <summary>
        /// Order date
        /// </summary>
        /// <example>20.09.2022</example>
        public string Date { get; set; }
        /// <summary>
        /// Delivery type
        /// </summary>
        /// <example>Nova Poshta</example>
        public string DeliveryType { get; set; }
        /// <summary>
        /// Delivery address
        /// </summary>
        /// <example></example>
        public string DeliveryAddress { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        /// <example>Cash</example>
        public string Payment { get; set; }
        /// <summary>
        /// Buyer Name
        /// </summary>
        /// <example>Nick Smith</example>
        public string BuyerName { get; set; }
        /// <summary>
        /// Buyer Phone
        /// </summary>
        /// <example>+380506953387</example>
        public string BuyerPhone { get; set; }
        /// <summary>
        /// Seller name
        /// </summary>
        /// <example>Fashion</example>
        public string SellerName { get; set; }
        /// <summary>
        /// Seller image
        /// </summary>
        /// <example>http://some-image.jpg</example>
        public string SellerImage { get; set; }
        /// <summary>
        /// Total price
        /// </summary>
        /// <example>2000</example>
        public float TotalPrice { get; set; }
        /// <summary>
        /// Site URL
        /// </summary>
        /// <example>https://some_example_url.com</example>
        public Uri Uri { get; set; }

        public IEnumerable<OrderProductRequest> Products { get; set; }
    }

    public class OrderProductRequest
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Dress</example>
        public string Name { get; set; }
        /// <summary>
        /// Product image
        /// </summary>
        /// <example>http://some_image.jpg</example>
        public string Image { get; set; }
        /// <summary>
        /// Product url slug
        /// </summary>
        /// <example>some-url-slug</example>
        public string UrlSlug { get; set; }
        /// <summary>
        /// Product count
        /// </summary>
        /// <example>2</example>
        public int Count { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        /// <example>3000</example>
        public float Price { get; set; }
        /// <summary>
        /// Product total price
        /// </summary>
        /// <example>6000</example>
        public float TotalPrice { get; set; }
    }
}
