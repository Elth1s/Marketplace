namespace WebAPI.ViewModels.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public string ShopName { get; set; }
        public string StatusName { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ProductImageForProductResponse> ProductImages { get; set; }
        public IEnumerable<ProductCharacteristicForProductResponse> ProductCharacteristics { get; set; }
    }
}
