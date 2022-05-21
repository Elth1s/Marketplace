﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class ShopIncludeCityWithCountrySpecification : Specification<Shop>, ISingleResultSpecification<Shop>
    {
        public ShopIncludeCityWithCountrySpecification()
        {
            Query.Include(a => a.City)
                .ThenInclude(f => f.Country)
                .AsSplitQuery();
        }
        public ShopIncludeCityWithCountrySpecification(int id)
        {
            Query.Where(a => a.Id == id)
                .Include(a => a.City)
                .ThenInclude(c => c.Country)
                .AsSplitQuery();
        }
    }
}
