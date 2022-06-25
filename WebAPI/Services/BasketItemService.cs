using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using DAL.Entities.Identity;

namespace WebAPI.Services
{
    public class BasketItemService: IBasketItemService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<BasketItem> _basketItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public BasketItemService(
         IRepository<BasketItem> basketItemRepository,
         IMapper mapper,
         UserManager<AppUser> userManager)
        {
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BasketResponse>> GetAllAsync(string userId)
        {
            var spec = new BasketItemIncludeFullInfoSpecification(userId);
            var baskets = await _basketItemRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<BasketResponse>>(baskets);
        }

        public async Task CreateAsync(BasketCreateRequest request,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            product.ProductNullChecking();

            var basketItem = _mapper.Map<BasketItem>(request);
            basketItem.UserId = userId;

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
