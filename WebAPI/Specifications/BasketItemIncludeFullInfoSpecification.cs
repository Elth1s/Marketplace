﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class BasketItemIncludeFullInfoSpecification:Specification<BasketItem>, ISingleResultSpecification<BasketItem>
    {

        public BasketItemIncludeFullInfoSpecification(string userId)
        {
            Query.Where(x=> x.UserId == userId)
                 .Include(p=>p.Product)
                 .AsSplitQuery();
        }

    }
}
