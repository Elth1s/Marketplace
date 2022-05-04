using Ardalis.Specification.EntityFrameworkCore;
using DAL.Data;

namespace DAL
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(MarketplaceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
