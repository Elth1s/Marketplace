using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

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
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        /// Returns category with the given identifier
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <response code="200">Getting category completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CategoryResponse))]
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
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category parent not found</response>
        /// <response code="500">An internal error has occurred</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            await _categoryService.CreateAsync(request);
            return Ok("Category created successfully");
        }

        /// <summary>
        /// Update an existing category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <param name="request">Category</param>
        /// <response code="200">Category update completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">Category parent or category not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            await _categoryService.UpdateAsync(id, request);
            return Ok("Category updated successfully");
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
            return Ok("Category deleted successfully");
        }
    }
}
