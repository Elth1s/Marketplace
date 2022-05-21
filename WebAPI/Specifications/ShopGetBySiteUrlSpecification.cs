using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class ShopGetBySiteUrlSpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopGetBySiteUrlSpecification(string siteUrl)
        {
            Query.Where(item => siteUrl == item.SiteUrl);
        }
    }
}
