namespace WebAPI.ViewModels.Request
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public int ShopId { get; set; }
        public int StatusId { get; set; }
        public int? CategoryId { get; set; }
        public ProductImageForProductCreateRequest[] ProductImages { get; set; }
        public ProductCharacteristicForProductCreateRequest[] ProductCharacteristics { get; set; }
    }

    public class ProductUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public int ShopId { get; set; }
        public int StatusId { get; set; }
        public int? CategoryId { get; set; }
        public ProductImageForProductUpdateRequest[] ProductImages { get; set; }
        public ProductCharacteristicForProductUpdateRequest[] ProductCharacteristics { get; set; }
    }
}
