namespace WebAPI.ViewModels.Response
{
    public class ProductCharacteristicResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int CharacteristicId { get; set; }
    }

    public class ProductCharacteristicForProductResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int CharacteristicId { get; set; }
    }
}