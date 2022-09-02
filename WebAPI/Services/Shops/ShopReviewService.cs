using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Interfaces.Shops;
using WebAPI.Specifications.Shops;
using WebAPI.ViewModels.Request.Shops;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Shops;

namespace WebAPI.Services.Shops
{
    public class ShopReviewService : IShopReviewService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<ShopReview> _shopReviewRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ShopReviewService(IRepository<ShopReview> shopReviewRepository,
                                 IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
                                 UserManager<AppUser> userManager,
                                 IMapper mapper,
                                 IRepository<Shop> shopRepository)
        {
            _shopReviewRepository = shopReviewRepository;
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _userManager = userManager;
            _mapper = mapper;
            _shopRepository = shopRepository;
        }

        public async Task CreateAsync(ShopReviewRequest request, string userId)
        {
            var shop = await _shopRepository.GetByIdAsync(request.ShopId);
            shop.ShopNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null && user.Id != userEmail.Id)
                throw new AppValidationException(
                    new ValidationError(nameof(AppUser.Email), _errorMessagesLocalizer["InvalidUserEmail"]));

            var isOrdered = await _userManager.IsOrderedInShopByUserAsync(userId, request.ShopId);
            if (!isOrdered)
                throw new AppException(_errorMessagesLocalizer["ReviewOrder"]);

            var review = _mapper.Map<ShopReview>(request);
            review.User = user;
            review.Shop = shop;

            await _shopReviewRepository.AddAsync(review);
            await _shopReviewRepository.SaveChangesAsync();
        }

        public async Task<PaginationResponse<ShopReviewResponse>> GetByShopIdAsync(ShopReviewForShopRequest request)
        {
            var shop = await _shopRepository.GetByIdAsync(request.ShopId);
            shop.ShopNullChecking();

            var reviewSpec = new ShopReviewGetByShopIdSpecification(shop.Id);
            var count = await _shopReviewRepository.CountAsync(reviewSpec);

            var result = new PaginationResponse<ShopReviewResponse>() { Count = count };

            reviewSpec = new ShopReviewGetByShopIdSpecification(
                shop.Id,
                (request.Page - 1) * request.RowsPerPage,
                    request.RowsPerPage);
            var reviews = await _shopReviewRepository.ListAsync(reviewSpec);

            result.Values = _mapper.Map<IEnumerable<ShopReviewResponse>>(reviews);

            return result;
        }
    }
}
