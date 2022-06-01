namespace WebAPI.ViewModels.Request
{
    public class ProductImageRequest
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductImageForProductCreateRequest
    {
        public string Name { get; set; }
    }

    public class ProductImageForProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}
