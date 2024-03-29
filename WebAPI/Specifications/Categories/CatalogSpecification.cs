﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Categories
{
    public class CatalogSpecification : Specification<Category>
    {
        public CatalogSpecification()
        {
            Query.Where(c => c.ParentId == null)
                 .Include(c => c.CategoryTranslations)
                 .AsSplitQuery();
        }

        public CatalogSpecification(int? parentId)
        {
            Query.Where(c => c.ParentId == parentId)
                 .Include(c => c.CategoryTranslations)
                 .Include(c => c.Children).ThenInclude(p => p.CategoryTranslations)
                 .Include(c => c.Children).ThenInclude(c => c.Children).ThenInclude(p => p.CategoryTranslations)
                 .AsSplitQuery();
        }
    }
}
