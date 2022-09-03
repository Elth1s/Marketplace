using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces.Orders;
using WebAPI.Specifications.Orders;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;
        private readonly IRepository<DeliveryType> _deliveryTypeRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<BasketItem> _basketItemRepository;
        private readonly IMapper _mapper;
        public OrderService(
               IRepository<Order> orderRepository,
               IRepository<OrderStatus> orderStatusRepository,
               IRepository<OrderProduct> orderProductRepository,
               IRepository<Product> productRepository,
               IRepository<DeliveryType> deliveryTypeRepository,
               IRepository<BasketItem> basketItemRepository,
               UserManager<AppUser> userManager,
               IMapper mapper
            )
        {
            _orderProductRepository = orderProductRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _productRepository = productRepository;
            _deliveryTypeRepository = deliveryTypeRepository;
            _basketItemRepository = basketItemRepository;
            _userManager = userManager;
            _mapper = mapper;
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
                        Price = item.ProductPrice
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

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var spec = new OrderIncludeFullInfoSpecification(id);
            var order = await _orderRepository.GetBySpecAsync(spec);
            order.OrderNullChecking();

            var result = _mapper.Map<OrderResponse>(order);
            result.OrderProductsResponse = _mapper.Map<IEnumerable<OrderProductResponse>>(order.OrderProducts);
            return result;
        }
    }
}
