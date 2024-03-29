﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Orders
{
    public class OrderUserIncludeFullInfoSpecification : Specification<Order>, ISingleResultSpecification<Order>
    {

        public OrderUserIncludeFullInfoSpecification(string userId)
        {
            Query.Where(o => o.UserId == userId)
                 .OrderByDescending(c => c.Id)
                 .Include(os => os.OrderStatus)
                 .ThenInclude(or => or.OrderStatusTranslations)
                 .Include(op => op.OrderProducts)
                 .ThenInclude(p => p.Product)
                 .ThenInclude(i => i.Images)
                 .Include(dt => dt.DeliveryType).ThenInclude(dtt => dtt.DeliveryTypeTranslations)
                 .AsSplitQuery();
        }
    }
}
