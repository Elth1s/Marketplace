using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Products
{
    public class ProductGetByUrlSlugSpecification : Specification<Product>, ISingleResultSpecification<Product>
    {
        public ProductGetByUrlSlugSpecification(string urlSlug)
        {
            Query.Where(item => urlSlug == item.UrlSlug.ToString())
                .Include(p => p.Category);
        }
    }
}
