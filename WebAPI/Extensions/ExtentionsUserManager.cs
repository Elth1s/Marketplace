using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class ExtentionsUserManager
    {
        public static async Task<AppUser> GetByPhoneNumberAsync(
            this UserManager<AppUser> userManager,
            string PhoneNumber,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == PhoneNumber && !u.IsDeleted, cancellationToken);
        }

        public static async Task<AppUser> GetByEmailAsync(
           this UserManager<AppUser> userManager,
           string email,
           CancellationToken cancellationToken = default)
        {
            email = userManager.NormalizeEmail(email);

            var user = await userManager.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == email && !u.IsDeleted, cancellationToken);
            return user;
        }

        public static IQueryable<AppUser> UserSearch(
            this UserManager<AppUser> userManager,
            string name,
            bool isAscOrder,
            string orderBy,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
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

        public static async Task<bool> IsOrderedByUserAsync(
            this UserManager<AppUser> userManager,
            string userId,
            int productId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = await userManager.Users
                                         .Where(u => u.Id == userId)
                                         .Include(u => u.Orders)
                                         .ThenInclude(o => o.OrderProducts)
                                         .CountAsync(u => u.Orders
                                                           .Any(o => o.OrderProducts
                                                           .Any(op => op.ProductId == productId)),
                                         cancellationToken);
            return query > 0;
        }

        public static async Task<bool> IsOrderedInShopByUserAsync(
            this UserManager<AppUser> userManager,
            string userId,
            int shopId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = await userManager.Users
                                         .Where(u => u.Id == userId)
                                         .Include(u => u.Orders)
                                         .ThenInclude(o => o.OrderProducts)
                                         .ThenInclude(op => op.Product)
                                         .CountAsync(u => u.Orders
                                                           .Any(o => o.OrderProducts
                                                           .Any(op => op.Product.ShopId == shopId)),
                                         cancellationToken);
            return query > 0;
        }

        public static async Task<AppUser> GetByIdWithIncludeInfo(
            this UserManager<AppUser> userManager,
            string userId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userManager.Users
                                         .Where(x => x.Id == userId)
                                         .Include(x => x.Shop)
                                         .Include(x => x.BasketItems)
                                         .Include(x => x.RefreshTokens)
                                         .Include(x => x.SelectedProducts)
                                         .Include(x => x.ReviewedProducts)
                                         .Include(x => x.ComparisonProducts)
                                         .SingleOrDefaultAsync(cancellationToken);
            return user;
        }

        public static async Task<AppUser> GetUserWithReviewedProductsAsync(
            this UserManager<AppUser> userManager,
            string userId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userManager.Users.Where(u => u.Id == userId)
                                              .Include(u => u.ReviewedProducts)
                                                    .ThenInclude(p => p.Shop)
                                              .Include(u => u.ReviewedProducts)
                                                    .ThenInclude(p => p.Status)
                                                        .ThenInclude(s => s.ProductStatusTranslations)
                                              .Include(u => u.ReviewedProducts)
                                                    .ThenInclude(p => p.Images)
                                              .SingleOrDefaultAsync(cancellationToken);
            return user;
        }

        public static async Task<AppUser> GetUserWithSelectedProductsAsync(
            this UserManager<AppUser> userManager,
            string userId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userManager.Users.Where(u => u.Id == userId)
                                              .Include(u => u.SelectedProducts)
                                                    .ThenInclude(p => p.Shop)
                                              .Include(u => u.SelectedProducts)
                                                    .ThenInclude(p => p.Status)
                                                        .ThenInclude(s => s.ProductStatusTranslations)
                                              .Include(u => u.SelectedProducts)
                                                    .ThenInclude(p => p.Images)
                                              .SingleOrDefaultAsync(cancellationToken);
            return user;
        }

        public static async Task<AppUser> GetUserWithComparisonProductsAsync(
            this UserManager<AppUser> userManager,
            string userId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userManager.Users.Where(u => u.Id == userId)
                                              .Include(u => u.ComparisonProducts)
                                                    .ThenInclude(p => p.Shop)
                                              .Include(u => u.ComparisonProducts)
                                                    .ThenInclude(p => p.Category)
                                                        .ThenInclude(c => c.CategoryTranslations)
                                              .Include(u => u.ComparisonProducts)
                                                    .ThenInclude(p => p.Images)
                                              .Include(u => u.ComparisonProducts)
                                                    .ThenInclude(p => p.FilterValueProducts)
                                                        .ThenInclude(f => f.FilterValue)
                                                            .ThenInclude(v => v.FilterValueTranslations)
                                               .Include(u => u.ComparisonProducts)
                                                    .ThenInclude(p => p.FilterValueProducts)
                                                        .ThenInclude(f => f.FilterValue)
                                                            .ThenInclude(v => v.FilterName)
                                                                .ThenInclude(n => n.Unit)
                                                                    .ThenInclude(u => u.UnitTranslations)
                                              .SingleOrDefaultAsync(cancellationToken);
            return user;
        }

        public static async Task<bool> IsProductSelectedByUserAsync(
            this UserManager<AppUser> userManager,
            string userId,
            int productId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var count = await userManager.Users.Where(u => u.Id == userId)
                                              .Include(u => u.SelectedProducts)
                                              .CountAsync(u => u.SelectedProducts.Any(p => p.Id == productId), cancellationToken);

            return count > 0;
        }

        public static async Task<AppUser> GetUserReviewsAsync(
           this UserManager<AppUser> userManager,
           string userId,
           CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await userManager.Users.Where(u => u.Id == userId)
                                                .Include(u => u.Orders)
                                                    .ThenInclude(o => o.OrderProducts)
                                                        .ThenInclude(op => op.Product)
                                                            .ThenInclude(p => p.Images)
                                                .Include(u => u.Reviews)
                                                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}

