using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Orders;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Controllers.Orders
{
    /// <summary>
    /// Order status controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusService _orderStatusService;
        private readonly IStringLocalizer<OrderStatusController> _orderStatusLocalizer;

        public OrderStatusController(IOrderStatusService orderStatusService, IStringLocalizer<OrderStatusController> orderStatusLocalizer)
        {
            _orderStatusService = orderStatusService;
            _orderStatusLocalizer = orderStatusLocalizer;
        }


        /// <summary>
        /// Returns all orders statuses
        /// </summary>
        /// <response code="200">Getting order statuses completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderStatusResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _orderStatusService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted order statuses
        /// </summary>
        /// <response code="200">Getting order statuses completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<OrderStatusResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] AdminSearchRequest request)
        {
            var result = await _orderStatusService.SearchOrderStatusesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns order status with the given identifier
        /// </summary>
        /// <param name="id">Order status identifier</param>
        /// <response code="200">Getting order status completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Order status not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OrderStatusFullInfoResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderStatusService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new order status
        /// </summary>
        /// <param name="request">New order status</param>
        /// <response code="200">Order status creation completed successfully</response>
        /// <response code="400">Order status name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] OrderStatusRequest request)
        {
            await _orderStatusService.CreateAsync(request);
            return Ok(_orderStatusLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing order status
        /// </summary>
        /// <param name="id">Order status identifier</param>
        /// <param name="request">Order status</param>
        /// <response code="200">Order status update completed successfully</response>
        /// <response code="400">Order status name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product status not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderStatusRequest request)
        {
            await _orderStatusService.UpdateAsync(id, request);
            return Ok(_orderStatusLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing order status
        /// </summary>
        /// <param name="id">Order status identifier</param>
        /// <response code="200">Order status deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product status not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderStatusService.DeleteAsync(id);
            return Ok(_orderStatusLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing order statuses
        /// </summary>
        /// <response code="200">Order statuses deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Order status not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOrderStatuses([FromQuery] IEnumerable<int> ids)
        {
            await _orderStatusService.DeleteOrderStatusesAsync(ids);
            return Ok(_orderStatusLocalizer["DeleteListSuccess"].Value);
        }
    }
}
