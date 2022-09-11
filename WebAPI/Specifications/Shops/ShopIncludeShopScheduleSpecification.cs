using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Shops
{
    public class ShopIncludeShopScheduleSpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopIncludeShopScheduleSpecification(int id)
        {
            Query.Where(s => s.Id == id)
                 .Include(a => a.ShopSchedule)
                    .ThenInclude(c => c.DayOfWeek)
                        .ThenInclude(d => d.DayOfWeekTranslations)
                 .AsSplitQuery();
        }
    }
}
