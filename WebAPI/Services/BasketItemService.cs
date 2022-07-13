using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.Specifications.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class BasketItemService : IBasketItemService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<BasketItem> _basketItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public BasketItemService(
         IRepository<BasketItem> basketItemRepository,
         IRepository<Product> productRepository,
         IMapper mapper,
         UserManager<AppUser> userManager)
        {
            _basketItemRepository = basketItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BasketResponse>> GetAllAsync(string userId)
        {
            var spec = new BasketItemIncludeFullInfoSpecification(userId);
            var baskets = await _basketItemRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<BasketResponse>>(baskets);
        }

        public async Task CreateAsync(BasketCreateRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new ProductIncludeFullInfoSpecification(request.UrlSlug);
            var product = await _productRepository.GetBySpecAsync(spec);
            product.ProductNullChecking();

            var basketItem = new BasketItem() { ProductId = product.Id, UserId = user.Id, Count = 1 };

            await _basketItemRepository.AddAsync(basketItem);
            await _basketItemRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var basketItem = await _basketItemRepository.GetByIdAsync(id);
            basketItem.BasketItemNullChecking();
            await _basketItemRepository.DeleteAsync(basketItem);
            await _basketItemRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int basketId, BasketUpdateRequest request, string userId)
        {
            var basket = await _basketItemRepository.GetByIdAsync(basketId);
            basket.BasketItemNullChecking();

            _mapper.Map(request, basket);

            await _basketItemRepository.UpdateAsync(basket);
            await _basketItemRepository.SaveChangesAsync();
        }
    }
}
