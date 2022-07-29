using AutoMapper;
using DAL;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces.Orders;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Services.Orders
{
    public class OrderStatusService : IOrderStatusService
    {

        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusService(IRepository<OrderStatus> orderStatus, IMapper mapper)
        {
            _orderStatusRepository = orderStatus;
            _mapper = mapper;
        }


        public async Task<IEnumerable<OrderStatusResponse>> GetAsync()
        {
            var orderStatus = await _orderStatusRepository.ListAsync();

            return _mapper.Map<IEnumerable<OrderStatusResponse>>(orderStatus);
        }

        public async Task<OrderStatusResponse> GetByIdAsync(int id)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
            orderStatus.OrderStatusNullChecking();

            return _mapper.Map<OrderStatusResponse>(orderStatus);
        }
        public async Task CreateAsync(OrderStatusRequest request)
        {
            var orderStatus = _mapper.Map<OrderStatus>(request);

            await _orderStatusRepository.AddAsync(orderStatus);
            await _orderStatusRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
            orderStatus.OrderStatusNullChecking();

            await _orderStatusRepository.DeleteAsync(orderStatus);
            await _orderStatusRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, OrderStatusRequest request)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
            orderStatus.OrderStatusNullChecking();

            _mapper.Map(request, orderStatus);

            await _orderStatusRepository.UpdateAsync(orderStatus);
            await _orderStatusRepository.SaveChangesAsync();
        }
    }
}
