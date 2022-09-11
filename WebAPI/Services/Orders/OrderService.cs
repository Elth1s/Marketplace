using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Orders;
using WebAPI.Specifications.Orders;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorLocalizer;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;
        private readonly IRepository<DeliveryType> _deliveryTypeRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<BasketItem> _basketItemRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public OrderService(
               IRepository<Order> orderRepository,
               IRepository<OrderStatus> orderStatusRepository,
               IRepository<OrderProduct> orderProductRepository,
               IRepository<Product> productRepository,
               IRepository<DeliveryType> deliveryTypeRepository,
               IRepository<BasketItem> basketItemRepository,
               IRepository<Shop> shopRepository,
               UserManager<AppUser> userManager,
               IMapper mapper,
               IStringLocalizer<ErrorMessages> errorLocalizer)
        {
            _orderProductRepository = orderProductRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _productRepository = productRepository;
            _deliveryTypeRepository = deliveryTypeRepository;
            _basketItemRepository = basketItemRepository;
            _shopRepository = shopRepository;
            _userManager = userManager;
            _mapper = mapper;
            _errorLocalizer = errorLocalizer;
        }

        public async Task CreateAsync(OrderCreateRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var order = _mapper.Map<Order>(request);
            order.User = user;

            var status = await _orderStatusRepository.GetByIdAsync(OrderStatusId.InProcess);
            status.OrderStatusNullChecking();
            order.OrderStatusId = OrderStatusId.InProcess;

            var deliveryType = await _deliveryTypeRepository.GetByIdAsync(request.DeliveryTypeId);
            deliveryType.DeliveryTypeNullChecking();
            order.DeliveryTypeId = deliveryType.Id;

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            foreach (var item in request.BasketItems)
            {
                await _orderProductRepository.AddAsync(
                    new OrderProduct()
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Count = item.Count,
                        Price = item.ProductDiscount ?? item.ProductPrice
                    });
                await _orderProductRepository.SaveChangesAsync();

                var basketItem = await _basketItemRepository.GetByIdAsync(item.Id);
                await _basketItemRepository.DeleteAsync(basketItem);
                await _basketItemRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            order.OrderNullChecking();

            await _orderRepository.DeleteAsync(order);
            await _orderRepository.SaveChangesAsync();

        }

        public async Task<IEnumerable<OrderResponse>> GetAsync()
        {
            var spec = new OrderIncludeFullInfoSpecification();
            var orders = await _orderRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<IEnumerable<OrderResponse>> GetForUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var spec = new OrderUserIncludeFullInfoSpecification(user.Id);
            var orders = await _orderRepository.ListAsync(spec);

            var resultOrders = orders.Select(o => new OrderResponse()
            {
                Id = o.Id,
                ConsumerFirstName = o.ConsumerFirstName,
                ConsumerSecondName = o.ConsumerSecondName,
                ConsumerEmail = o.ConsumerEmail,
                ConsumerPhone = o.ConsumerPhone,
                OrderStatusName = o.OrderStatus.OrderStatusTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name,
                DeliveryType = o.DeliveryType.DeliveryTypeTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name,
                TotalPrice = o.OrderProducts.Sum(o => o.Price * o.Count),
                OrderProductsResponse = _mapper.Map<IEnumerable<OrderProductResponse>>(o.OrderProducts)
            });

            return resultOrders;
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var spec = new OrderIncludeFullInfoSpecification(id);
            var order = await _orderRepository.GetBySpecAsync(spec);
            order.OrderNullChecking();

            var result = _mapper.Map<OrderResponse>(order);
            result.OrderProductsResponse = _mapper.Map<IEnumerable<OrderProductResponse>>(order.OrderProducts);
            return result;
        }

        public async Task<SearchResponse<OrderResponse>> AdminSellerSearchAsync(SellerSearchRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var shop = await _shopRepository.GetByIdAsync(user.ShopId.Value);
            shop.ShopNullChecking();

            var spec = new OrderSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy, request.IsSeller, shop.Id);
            var count = await _orderRepository.CountAsync(spec);
            spec = new OrderSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                request.IsSeller,
                user.ShopId,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var orders = await _orderRepository.ListAsync(spec);
            var mappedOrders = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            var response = new SearchResponse<OrderResponse>() { Count = count, Values = mappedOrders };

            return response;
        }

        public async Task CancelOrderAsync(int id, string userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            order.OrderNullChecking();

            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            if (order.UserId != userId)
                throw new AppException(_errorLocalizer["DontHavePermission"]);

            if (order.OrderStatusId == OrderStatusId.Canceled)
                throw new AppException(_errorLocalizer["OrderAlreadyCanceled"]);

            order.OrderStatusId = OrderStatusId.Canceled;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }
    }
}
