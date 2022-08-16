using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using WebAPI.Extensions;
using WebAPI.Interfaces.Orders;
using WebAPI.Specifications.Orders;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
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
            var spec = new OrderStatusIncludeInfoSpecification();
            var orderStatus = await _orderStatusRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<OrderStatusResponse>>(orderStatus);
        }
        public async Task<AdminSearchResponse<OrderStatusResponse>> SearchOrderStatusesAsync(AdminSearchRequest request)
        {
            var spec = new OrderStatusSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _orderStatusRepository.CountAsync(spec);
            spec = new OrderStatusSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var statuses = await _orderStatusRepository.ListAsync(spec);
            var mappedStatuses = _mapper.Map<IEnumerable<OrderStatusResponse>>(statuses);
            var response = new AdminSearchResponse<OrderStatusResponse>() { Count = count, Values = mappedStatuses };

            return response;
        }

        public async Task<OrderStatusFullInfoResponse> GetByIdAsync(int id)
        {
            var spec = new OrderStatusIncludeInfoSpecification(id);
            var orderStatus = await _orderStatusRepository.GetBySpecAsync(spec);
            orderStatus.OrderStatusNullChecking();

            return _mapper.Map<OrderStatusFullInfoResponse>(orderStatus);
        }
        public async Task CreateAsync(OrderStatusRequest request)
        {
            var specName = new OrderStatusGetByNameSpecification(request.EnglishName, LanguageId.English);
            var orderStatusEnNameExist = await _orderStatusRepository.GetBySpecAsync(specName);
            if (orderStatusEnNameExist != null)
                orderStatusEnNameExist.OrderStatusWithEnglishNameChecking(nameof(OrderStatusRequest.EnglishName));

            specName = new OrderStatusGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var orderStatusUkNameExist = await _orderStatusRepository.GetBySpecAsync(specName);
            if (orderStatusUkNameExist != null)
                orderStatusUkNameExist.OrderStatusWithUkrainianNameChecking(nameof(OrderStatusRequest.UkrainianName));

            var orderStatus = _mapper.Map<OrderStatus>(request);

            await _orderStatusRepository.AddAsync(orderStatus);
            await _orderStatusRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, OrderStatusRequest request)
        {
            var spec = new OrderStatusIncludeInfoSpecification(id);
            var orderStatus = await _orderStatusRepository.GetBySpecAsync(spec);
            orderStatus.OrderStatusNullChecking();

            var specName = new OrderStatusGetByNameSpecification(request.EnglishName, LanguageId.English);
            var orderStatusEnNameExist = await _orderStatusRepository.GetBySpecAsync(specName);
            if (orderStatusEnNameExist != null && orderStatusEnNameExist.Id != orderStatus.Id)
                orderStatusEnNameExist.OrderStatusWithEnglishNameChecking(nameof(OrderStatusRequest.EnglishName));

            specName = new OrderStatusGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var orderStatusUkNameExist = await _orderStatusRepository.GetBySpecAsync(specName);
            if (orderStatusUkNameExist != null && orderStatusUkNameExist.Id != orderStatus.Id)
                orderStatusUkNameExist.OrderStatusWithUkrainianNameChecking(nameof(OrderStatusRequest.UkrainianName));

            orderStatus.OrderStatusTranslations.Clear();
            _mapper.Map(request, orderStatus);

            await _orderStatusRepository.UpdateAsync(orderStatus);
            await _orderStatusRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
            orderStatus.OrderStatusNullChecking();

            await _orderStatusRepository.DeleteAsync(orderStatus);
            await _orderStatusRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderStatusesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var status = await _orderStatusRepository.GetByIdAsync(item);
                await _orderStatusRepository.DeleteAsync(status);
            }
            await _orderStatusRepository.SaveChangesAsync();
        }
    }
}
