namespace WebAPI.ViewModels.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
        public int CharacteristicId { get; set; }
    }
}
