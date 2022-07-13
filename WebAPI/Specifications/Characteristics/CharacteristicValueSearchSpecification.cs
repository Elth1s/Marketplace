﻿using Ardalis.Specification;
using DAL.Entities;
using WebAPI.Extensions;

namespace WebAPI.Specifications.Characteristics
{
    public class CharacteristicValueSearchSpecification : Specification<CharacteristicValue>
    {
        public CharacteristicValueSearchSpecification(string name, bool isAscOrder, string orderBy)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Where(item => item.Value.Contains(name));

            Query.Include(c => c.CharacteristicName)
                .AsSplitQuery();

            if (orderBy == "characteristicName")
            {
                if (isAscOrder)
                    Query.OrderBy(c => c.CharacteristicName.Name);
                else
                    Query.OrderByDescending(c => c.CharacteristicName.Name);
            }
            else
            {
                if (isAscOrder)
                    Query.OrderBy(orderBy);
                else
                    Query.OrderByDescending(orderBy);
            }
        }
    }
}
