﻿using Ardalis.Specification;
using DAL.Entities;

namespace WebAPI.Specifications.Questions
{
    public class QuestionsByProductIdSpecification : Specification<Question>
    {
        public QuestionsByProductIdSpecification(int productId, int? skip = null, int? take = null)
        {
            Query.Where(r => r.ProductId == productId)
                 .Include(r => r.Images)
                 .Include(r => r.Votes)
                 .Include(r => r.Replies)
                 .OrderByDescending(r => r.Date)
                 .AsSplitQuery();

            if (skip.HasValue)
                Query.Skip(skip.Value);

            if (take.HasValue)
                Query.Take(take.Value);
        }
    }
}
