﻿using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Interfaces.Products
{
    public interface IProductStatusService
    {
        Task<IEnumerable<ProductStatusResponse>> GetAsync();
        Task<SearchResponse<ProductStatusResponse>> SearchProductStatusesAsync(AdminSearchRequest request);
        Task<ProductStatusFullInfoResponse> GetByIdAsync(int id);
        Task CreateAsync(ProductStatusRequest request);
        Task UpdateAsync(int id, ProductStatusRequest request);
        Task DeleteAsync(int id);
        Task DeleteProductStatusesAsync(IEnumerable<int> ids);
    }
}
