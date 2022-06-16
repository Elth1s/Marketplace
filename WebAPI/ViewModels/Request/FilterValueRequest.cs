namespace WebAPI.ViewModels.Request
{
    public class FilterValueRequest
    {
        public string Value { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public int FilterNameId { get; set; }
    }
}
