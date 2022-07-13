using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces.Orders;
using WebAPI.Specifications.Orders;
using WebAPI.ViewModels.Request.Order;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;
        private readonly IMapper _mapper;
        public OrderService(
               IRepository<Order> orderRepository,
               IRepository<OrderStatus> orderStatusRepository,
               IRepository<OrderProduct> orderProductRepository,
               IMapper mapper
            )
        {
            _orderProductRepository = orderProductRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(OrderCreateRequest request)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(request.OrderStatusId);
            orderStatus.OrderStatusNullChecking();

            var order = _mapper.Map<Order>(request);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            foreach (var orderProduct in request.OrderProductsCreate)
            {
                await _orderProductRepository.AddAsync(
                    new OrderProduct()
                    {
                        Count = orderProduct.Count,
                        OrderId = order.Id,
                        Price = orderProduct.Price,
                        ProductId = orderProduct.ProductId,
                    });
            }
            await _orderProductRepository.SaveChangesAsync();
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


            return _mapper.Map<OrderResponse>(order);
        }
    }
}
