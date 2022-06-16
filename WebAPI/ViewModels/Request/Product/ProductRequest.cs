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
        public int CategoryId { get; set; }

        public IEnumerable<FilterValueProductCreate> FiltersValue { get; set; }
    }

    public class FilterValueProductCreate
    {
        public int Id { get; set; }
        public float? CustomValue { get; set; }
    }
}
