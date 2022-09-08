using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Categories;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Filters;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Controllers
{
    /// <summary>
    /// The category controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<CategoryController> _categoryLocalizer;

        public CategoryController(ICategoryService categoryService, IStringLocalizer<CategoryController> categoryLocalizer)
        {
            _categoryService = categoryService;
            _categoryLocalizer = categoryLocalizer;
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <response code="200">Getting categories completed successfully</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(200, Type = typeof(IEnumerable<CategoryResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("Get")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _categoryService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of sorted categories
        /// </summary>
        /// <response code="200">Getting categories completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SearchResponse<CategoryResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchCategories([FromQuery] AdminSearchRequest request)
        {
            var result = await _categoryService.SearchCategoriesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Return of catalog with products
        /// </summary>
        /// <response code="200">Getting catalog with products completed successfully</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CatalogWithProductsResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetCatalogWithProducts")]
        public async Task<IActionResult> GetCatalogWithProducts([FromQuery] CatalogWithProductsRequest request)
        {
            var result = await _categoryService.GetCatalogWithProductsAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Return of catalog with products
        /// </summary>
        /// <response code="200">Getting catalog with products completed successfully</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FullCatalogItemResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetCategoriesByProducts")]
        public async Task<IActionResult> GetCategoriesByProducts([FromQuery] SearchProductsRequest request)
        {
            var result = await _categoryService.GetCategoriesByProductsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Return more products
        /// </summary>
        /// <response code="200">Getting products completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductCatalogResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetMoreProducts")]
        public async Task<IActionResult> GetMoreProducts([FromQuery] CatalogWithProductsRequest request)
        {
            var result = await _categoryService.GetMoreProductsAsync(request, UserId);
            return Ok(result);
        }

        /// <summary>
        /// Return of catalog
        /// </summary>
        /// <response code="200">Getting catalog completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CatalogItemResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetCatalog")]
        public async Task<IActionResult> GetCatalog()
        {
            var result = await _categoryService.GetCatalogAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return of full catalog
        /// </summary>
        /// <response code="200">Getting full catalog completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FullCatalogItemResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetFullCatalog")]
        public async Task<IActionResult> GetFullCatalog()
        {
            var result = await _categoryService.GetFullCatalogAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return filters of category
        /// </summary>
        /// <response code="200">Getting filters of category completed successfully</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilterNameValuesResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetFiltersByCategory")]
        public async Task<IActionResult> GetFiltersByCategory([FromQuery] string urlSlug)
        {
            var result = await _categoryService.GetFiltersByCategoryAsync(urlSlug);
            return Ok(result);
        }

        /// <summary>
        /// Return filters of category
        /// </summary>
        /// <response code="200">Getting filters of category completed successfully</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilterNameValuesResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetFiltersByCategoryId")]
        public async Task<IActionResult> GetFiltersByCategory([FromQuery] int id)
        {
            var result = await _categoryService.GetFiltersByCategoryAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Return filters of category id
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <response code="200">Getting filters of category id completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilterNameValuesResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetFiltersByCategory/{id}")]
        public async Task<IActionResult> GetFiltersByCategoryId(int id)
        {
            var result = await _categoryService.GetFiltersByCategoryIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Return parents of category
        /// </summary>
        /// <response code="200">Getting parents of category completed successfully</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CatalogItemResponse>))]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetParents")]
        public async Task<IActionResult> GetParents([FromQuery] string urlSlug)
        {
            var result = await _categoryService.GetParentsAsync(urlSlug);
            return Ok(result);
        }

        /// <summary>
        /// Returns all categories for select
        /// </summary>
        /// <response code="200">Getting categories for select completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryForSelectResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetForSelect")]
        public async Task<IActionResult> GetForSelectCategory()
        {
            var result = await _categoryService.GetForSelectAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns categories without children for select
        /// </summary>
        /// <response code="200">Getting categories without children for select completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryForSelectResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetWithoutChildren")]
        public async Task<IActionResult> GetCategoriesWithoutChildren()
        {
            var result = await _categoryService.GetCategoriesWithoutChildrenAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns category with the given identifier
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <response code="200">Getting category completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CategoryFullInfoResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="request">New category</param>
        /// <response code="200">Category creation completed successfully</response>
        /// <response code="400">Category name or URL slug not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category parent not found</response>
        /// <response code="500">An internal error has occurred</response>        
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            await _categoryService.CreateAsync(request);
            return Ok(_categoryLocalizer["CreateSuccess"].Value);
        }

        /// <summary>
        /// Update an existing category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <param name="request">Category</param>
        /// <response code="200">Category update completed successfully</response>
        /// <response code="400">Category name or URL slug not unique</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category parent or category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            await _categoryService.UpdateAsync(id, request);
            return Ok(_categoryLocalizer["UpdateSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <response code="200">Category deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok(_categoryLocalizer["DeleteSuccess"].Value);
        }

        /// <summary>
        /// Delete an existing categories
        /// </summary>
        /// <response code="200">Categories deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategories([FromQuery] IEnumerable<int> ids)
        {
            await _categoryService.DeleteCategoriesAsync(ids);
            return Ok(_categoryLocalizer["DeleteListSuccess"].Value);
        }
    }
}
