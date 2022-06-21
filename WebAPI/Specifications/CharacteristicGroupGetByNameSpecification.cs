﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications
{
    public class CharacteristicGroupGetByNameSpecification : Specification<CharacteristicGroup>, ISingleResultSpecification<CharacteristicGroup>
    {
        public CharacteristicGroupGetByNameSpecification(string name)
        {
            Query.Where(item => name == item.Name);
        }
    }
}
