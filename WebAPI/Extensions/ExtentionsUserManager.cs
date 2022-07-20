using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class ExtentionsUserManager
    {
        public static async Task<AppUser> FindByPhoneNumberAsync(this UserManager<AppUser> userManager, string PhoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == PhoneNumber, cancellationToken);
        }
        public static IQueryable<AppUser> UserSearch(
            this UserManager<AppUser> userManager,
            string name,
            bool isAscOrder,
            string orderBy,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = userManager.Users;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(item => item.FirstName.Contains(name) || item.SecondName.Contains(name));

            if (isAscOrder)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }
    }
}

