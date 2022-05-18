namespace WebAPI.ViewModels.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ParentName { get; set; }
        public string CharacteristicName { get; set; }
    }
}
