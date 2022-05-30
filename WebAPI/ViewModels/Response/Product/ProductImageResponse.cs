namespace WebAPI.ViewModels.Response
{
    public class ProductImageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductImageForProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
