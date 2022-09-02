using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
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
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Returns sales 
        /// </summary>
        /// <response code="200">Getting sales completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Sales not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SaleResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
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
        /// <response code="404">Sales not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SaleResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int saleId)
        {
            var result = await _saleService.GetSaleByIdAsync(saleId);
            return Ok(result);
        }

        /// <summary>
        /// Create new sale item
        /// </summary>
        /// <param name="request">Sale Request</param>
        /// <response code="200">Sale add completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User or Sale not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequest request)
        {
            await _saleService.CreateAsync(request, UserId);
            return Ok();
        }

        /// <summary>
        /// Delete an existing sale
        /// </summary>
        /// <param name="saleId">Sale identifier</param>
        /// <response code="200">Sale update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">Sale not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpDelete("Delete/{saleId}")]
        public async Task<IActionResult> DeleteSale(int saleId)
        {
            await _saleService.DeleteSaleAsync(saleId);
            return Ok();
        }



    }
}
