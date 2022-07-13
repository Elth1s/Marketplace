namespace WebAPI.ViewModels.Request.Order
{
    /// <summary>
    /// Order class to create ordera 
    /// </summary>
    public class OrderCreateRequest
    {
        /// <summary>
        /// Consumer First Name
        /// </summary>
        /// <example>Novak</example>
        public string ConsumerFirstName { get; set; }
        /// <summary>
        /// Consumer Second Name
        /// </summary>
        /// <example>Vova</example>
        public string ConsumerSecondName { get; set; }
        /// <summary>
        /// Consumer Phone
        /// </summary>
        /// <example>+380971233214</example>
        public string ConsumerPhone { get; set; }
        /// <summary>
        /// Consumer Phone
        /// </summary>
        /// <example>novak@gmail.com</example>
        public string ConsumerEmail { get; set; }

        /// <summary>
        /// Order Status Id
        /// </summary>
        /// <example>1</example>
        public int OrderStatusId { get; set; }

        /// <summary>
        ///  User Id
        /// </summary>
        /// <example>1</example>
        public string UserId { get; set; }

        public IEnumerable<OrderProductCreate> OrderProductsCreate { get; set; }
    }

    public class OrderProductCreate
    {
        /// <summary>
        ///  Count Order Product
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }
        /// <summary>
        ///  Price Order Product
        /// </summary>
        /// <example>1000</example>
        public int Price { get; set; }


        /// <summary>
        ///   Order Id
        /// </summary>
        /// <example>1</example>
        public int OrderId { get; set; }
        /// <summary>
        ///   Product Id
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }


    }

}

   


