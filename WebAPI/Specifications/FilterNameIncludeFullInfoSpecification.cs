﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class FilterNameIncludeFullInfoSpecification : Specification<FilterName>, ISingleResultSpecification<FilterName>
    {
        public FilterNameIncludeFullInfoSpecification()
        {
            Query.Include(o => o.FilterGroup)
                 .Include(o => o.Unit)
                 .AsSplitQuery();
        }
        public FilterNameIncludeFullInfoSpecification(int id)
        {
            Query.Where(o => o.Id == id)
                 .Include(o => o.FilterGroup)
                 .Include(o => o.Unit)
                 .AsSplitQuery();
        }

    }
}
