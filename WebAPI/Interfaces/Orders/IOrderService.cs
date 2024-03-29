﻿using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAsync();
        Task<IEnumerable<OrderResponse>> GetForUserAsync(string userId);
        Task<SearchResponse<OrderResponse>> AdminSellerSearchAsync(SellerSearchRequest request, string userId);
        Task<OrderResponse> GetByIdAsync(int id);
        Task CancelOrderAsync(int id, string userId);
        Task CreateAsync(OrderCreateRequest request, string userId);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, UpdateOrderRequest request);
        Task SendOrderEmail(int id, string userId);

    }
}
