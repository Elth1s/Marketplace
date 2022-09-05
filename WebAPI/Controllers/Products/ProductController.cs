﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Controllers.Products
{
    /// <summary>
    /// Product controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IStringLocalizer<ProductController> _productLocalizer;
        private readonly IProductService _productService;
        public ProductController(IProductService productService, IStringLocalizer<ProductController> productLocalizer)
        {
            _productService = productService;
            _productLocalizer = productLocalizer;
        }


        /// <summary>
        /// Returns all products
        /// </summary>
        /// <response code="200">Getting products completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted products
        /// </summary>
        /// <response code="200">Getting products completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<ProductResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("AdminSellerSearch")]
        public async Task<IActionResult> AdminSellerSearchProducts([FromQuery] SellerSearchRequest request)
        {
            var result = await _productService.AdminSellerSearchProductsAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted products
        /// </summary>
        /// <response code="200">Getting products completed successfully</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<ProductCatalogResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductRequest request)
        {
            var result = await _productService.SearchProductsAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns the requested product with the given identifier
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <response code="200">Getting product completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Returns the requested product with category parents identifier
        /// </summary>
        /// <response code="200">Getting product completed successfully</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductWithCategoryParentsResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetByUrlSlug")]
        public async Task<IActionResult> GetByUrlSlug([FromQuery] string urlSlug)
        {
            var result = await _productService.GetByUrlSlugAsync(urlSlug, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns the similar products 
        /// </summary>
        /// <response code="200">Getting similar product completed successfully</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductCatalogResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetSimilarProducts")]
        public async Task<IActionResult> GetSimilarProducts([FromQuery] string urlSlug)
        {
            var result = await _productService.GetSimilarProductsAsync(urlSlug, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="request">New product</param>
        /// <response code="200">Product creation completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Shop, product status or category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            await _productService.CreateAsync(request, UserId);
            return Ok(_productLocalizer["CreateSuccess"].Value);
        }

        //[Authorize(Roles = "Admin,Seller")]
        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest request)
        //{
        //    await _productService.UpdateAsync(id, request);
        //    return Ok("Product updated successfully");
        //}

        /// <summary>
        /// Delete an existing product
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <response code="200">Product deletion completed successfully</response>
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
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok(_productLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing products
        /// </summary>
        /// <response code="200">Products deletion completed successfully</response>
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProducts([FromQuery] IEnumerable<int> ids)
        {
            await _productService.DeleteProductsAsync(ids);
            return Ok(_productLocalizer["DeleteListSuccess"].Value);
        }

        /// <summary>
        /// Select product
        /// </summary>
        /// <param name="productSlug">Product slug</param>
        /// <response code="200">The change of the selected product completed successfully</response>
        /// <response code="404">Product or user not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("ChangeSelectProduct/{productSlug}")]
        public async Task<IActionResult> ChangeSelectProduct(string productSlug)
        {
            await _productService.ChangeSelectProductAsync(productSlug, UserId);
            return Ok(_productLocalizer["ChangeSelectSuccess"].Value);
        }

        /// <summary>
        /// Returns user selected products
        /// </summary>
        /// <response code="200">Getting product completed successfully</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetSelectedProducts")]
        public async Task<IActionResult> GetSelectedProducts()
        {
            var result = await _productService.GetSelectedProductsAsync(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Returns user reviewed products
        /// </summary>
        /// <response code="200">Getting product completed successfully</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("GetReviewedProducts")]
        public async Task<IActionResult> GetReviewedProducts()
        {
            var result = await _productService.GetReviewedProductsAsync(UserId);
            return Ok(result);
        }
    }
}
