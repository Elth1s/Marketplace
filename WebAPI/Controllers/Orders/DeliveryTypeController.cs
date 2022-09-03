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
    /// Delivery type controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IDeliveryTypeService _deliveryTypeService;
        private readonly IStringLocalizer<DeliveryTypeController> _deliveryTypeLocalizer;

        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService, IStringLocalizer<DeliveryTypeController> deliveryTypeLocalizer)
        {
            _deliveryTypeService = deliveryTypeService;
            _deliveryTypeLocalizer = deliveryTypeLocalizer;
        }


        /// <summary>
        /// Returns all delivery types
        /// </summary>
        /// <response code="200">Getting delivery types completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeliveryTypeResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _deliveryTypeService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted delivery types
        /// </summary>
        /// <response code="200">Getting delivery types completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<DeliveryTypeResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] AdminSearchRequest request)
        {
            var result = await _deliveryTypeService.SearchDeliveryTypesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns delivery type with the given identifier
        /// </summary>
        /// <param name="id">Delivery type identifier</param>
        /// <response code="200">Getting delivery type completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Delivery type not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DeliveryTypeFullInfoResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _deliveryTypeService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new order status
        /// </summary>
        /// <param name="request">New delivery type</param>
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
        public async Task<IActionResult> Create([FromBody] DeliveryTypeRequest request)
        {
            await _deliveryTypeService.CreateAsync(request);
            return Ok(_deliveryTypeLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing delivery type
        /// </summary>
        /// <param name="id">Delivery type identifier</param>
        /// <param name="request">Delivery type</param>
        /// <response code="200">Delivery type update completed successfully</response>
        /// <response code="400">Delivery type name not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Delivery type not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DeliveryTypeRequest request)
        {
            await _deliveryTypeService.UpdateAsync(id, request);
            return Ok(_deliveryTypeLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing delivery type
        /// </summary>
        /// <param name="id">Delivery type identifier</param>
        /// <response code="200">Delivery type deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Delivery type not found</response>
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
            await _deliveryTypeService.DeleteAsync(id);
            return Ok(_deliveryTypeLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing delivery type
        /// </summary>
        /// <response code="200">Delivery types deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Delivery type not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProductStatuses([FromQuery] IEnumerable<int> ids)
        {
            await _deliveryTypeService.DeleteDeliveryTypesAsync(ids);
            return Ok(_deliveryTypeLocalizer["DeleteListSuccess"].Value);
        }
    }
}
