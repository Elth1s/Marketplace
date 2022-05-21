namespace WebAPI.ViewModels.Request
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
        public int CharacteristicId { get; set; }
    }
}
