namespace WebAPI.ViewModels.Response
{
    public class FilterValueResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public string FilterName { get; set; }
    }
}
