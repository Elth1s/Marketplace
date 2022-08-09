using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Controllers.Products
{
    /// <summary>
    /// Product status controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : Controller
    {
        private readonly IProductStatusService _productStatusService;
        private readonly IStringLocalizer<ProductStatusController> _productStatusLocalizer;


        public ProductStatusController(IProductStatusService productStatusService,
            IStringLocalizer<ProductStatusController> productStatusLocalizer)
        {
            _productStatusService = productStatusService;
            _productStatusLocalizer = productStatusLocalizer;
        }

        /// <summary>
        /// Returns all product statuses
        /// </summary>
        /// <response code="200">Getting product statuses completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductStatusResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _productStatusService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted product statuses
        /// </summary>
        /// <response code="200">Getting product statuses completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdminSearchResponse<ProductStatusResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchProductStatuses([FromQuery] AdminSearchRequest request)
        {
            var result = await _productStatusService.SearchProductStatusesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Returns product status with the given identifier
        /// </summary>
        /// <param name="id">Product status identifier</param>
        /// <response code="200">Getting product status completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product status not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductStatusResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productStatusService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new product status
        /// </summary>
        /// <param name="request">New product status</param>
        /// <response code="200">Product status creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductStatusRequest request)
        {
            await _productStatusService.CreateAsync(request);
            return Ok(_productStatusLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing product status
        /// </summary>
        /// <param name="id">Product status identifier</param>
        /// <param name="request">Product status</param>
        /// <response code="200">Product status update completed successfully</response>
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
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductStatusRequest request)
        {
            await _productStatusService.UpdateAsync(id, request);
            return Ok(_productStatusLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing product status
        /// </summary>
        /// <param name="id">Product status identifier</param>
        /// <response code="200">Product status deletion completed successfully</response>
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
            await _productStatusService.DeleteAsync(id);
            return Ok(_productStatusLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing product statuses
        /// </summary>
        /// <response code="200">Product statuses deletion completed successfully</response>
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProductStatuses([FromQuery] IEnumerable<int> ids)
        {
            await _productStatusService.DeleteProductStatusesAsync(ids);
            return Ok(_productStatusLocalizer["DeleteListSuccess"].Value);
        }
    }
}
