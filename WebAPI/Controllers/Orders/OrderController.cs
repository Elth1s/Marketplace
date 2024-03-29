﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Orders;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Controllers.Orders
{
    /// <summary>
    /// Order controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IStringLocalizer<OrderController> _orderLocalizer;
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService, IStringLocalizer<OrderController> orderLocalizer)
        {
            _orderService = orderService;
            _orderLocalizer = orderLocalizer;
        }

        /// <summary>
        /// Returns all orders to the user
        /// </summary>
        /// <response code="200">Getting orders completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetForUser")]
        public async Task<IActionResult> GetForUser()
        {
            var result = await _orderService.GetForUserAsync(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted orders
        /// </summary>
        /// <response code="200">Getting orders completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<OrderResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("AdminSellerSearch")]
        public async Task<IActionResult> AdminSellerSearchOreders([FromQuery] SellerSearchRequest request)
        {
            var result = await _orderService.AdminSellerSearchAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns all orders
        /// </summary>
        /// <response code="200">Getting orders completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _orderService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns the requested order with the given identifier
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <response code="200">Getting order completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetByIdOrder/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="request">New order</param>
        /// <response code="200">Order creation completed successfully</response>
        /// <response code="404">Shop, product status or category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest request)
        {
            await _orderService.CreateAsync(request, UserId);
            return Ok(_orderLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Cancel order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <response code="200">Order canceling completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Order not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("CancelOrder/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrderAsync(id, UserId);
            return Ok(_orderLocalizer["CancelSuccess"].Value);
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <param name="request">Order update info</param>
        /// <response code="200">Order updating completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Order not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
        {
            await _orderService.UpdateAsync(id, request);
            return Ok(_orderLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <response code="200">Order deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok(_orderLocalizer["DeleteSuccess"].Value);
        }

    }
}
