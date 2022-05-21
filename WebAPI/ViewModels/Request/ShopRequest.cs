namespace WebAPI.ViewModels.Request
{
    public class ShopRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string SiteUrl { get; set; }

        public int CityId { get; set; }
    }
}
