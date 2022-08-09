using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces.Products;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Controllers.Products
{
    /// <summary>
    /// Product image controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        private readonly IStringLocalizer<ProductImageController> _productImageLocalizer;

        public ProductImageController(IProductImageService productImageService, IStringLocalizer<ProductImageController> productImageLocalizer)
        {
            _productImageService = productImageService;
            _productImageLocalizer = productImageLocalizer;
        }

        /// <summary>
        /// Create new product image
        /// </summary>
        /// <param name="base64">New product image</param>
        /// <response code="200"></response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductImageResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] string base64)
        {
            var result = await _productImageService.CreateAsync(base64);
            return Created(_productImageLocalizer["CreateSuccess"].Value, result);
        }

        /// <summary>
        /// Delete an existing product image
        /// </summary>
        /// <param name="id">Product image identifier</param>
        /// <response code="200">Product image deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product image not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productImageService.DeleteAsync(id);
            return Ok(_productImageLocalizer["DeleteSuccess"].Value);
        }
    }
}
