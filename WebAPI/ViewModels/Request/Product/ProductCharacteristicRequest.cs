namespace WebAPI.ViewModels.Request
{
    public class ProductCharacteristicRequest
    {
        public string Value { get; set; }
        public int CharacteristicId { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductCharacteristicForProductCreateRequest
    {
        public string Value { get; set; }
        public int CharacteristicId { get; set; }
    }

    public class ProductCharacteristicForProductUpdateRequest
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int CharacteristicId { get; set; }
        public int ProductId { get; set; }
    }
}
