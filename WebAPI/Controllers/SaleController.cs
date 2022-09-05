using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
{

    /// <summary>
    /// The sale controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IStringLocalizer<SaleController> _saleLocalizer;

        public SaleController(ISaleService saleService,
                              IStringLocalizer<SaleController> saleLocalizer)
        {
            _saleService = saleService;
            _saleLocalizer = saleLocalizer;
        }

        /// <summary>
        /// Returns sales 
        /// </summary>
        /// <response code="200">Getting sales completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Sales not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SaleResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _saleService.GetSalesAsync();
            return Ok(result);
        }


        /// <summary>
        /// Returns sales by ID
        /// </summary>
        /// <response code="200">Getting sales completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Sales not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SaleResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{saleId}")]
        public async Task<IActionResult> GetById(int saleId)
        {
            var result = await _saleService.GetSaleByIdAsync(saleId);
            return Ok(result);
        }

        /// <summary>
        /// Create new sale
        /// </summary>
        /// <param name="request">New sale</param>
        /// <response code="200">Sale created completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequest request)
        {
            await _saleService.CreateAsync(request);
            return Ok(_saleLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update sale
        /// </summary> 
        /// <param name="id">Sale identifier</param>
        /// <param name="request">Sale Request</param>
        /// <response code="200">Sale update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Sale not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] SaleRequest request)
        {
            await _saleService.UpdateAsync(id, request);
            return Ok(_saleLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing sale
        /// </summary>
        /// <param name="saleId">Sale identifier</param>
        /// <response code="200">Sale update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Sale not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{saleId}")]
        public async Task<IActionResult> DeleteSale(int saleId)
        {
            await _saleService.DeleteSaleAsync(saleId);
            return Ok(_saleLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing sales
        /// </summary>
        /// <response code="200">Sales deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Sale not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteSales([FromQuery] IEnumerable<int> ids)
        {
            await _saleService.DeleteSalesAsync(ids);
            return Ok(_saleLocalizer["DeleteListSuccess"].Value);
        }

    }
}
