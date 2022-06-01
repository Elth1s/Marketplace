using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _categoryService.GetAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetForSelect")]
        public async Task<IActionResult> GetForSelectCategory()
        {
            var result = await _categoryService.GetForSelectAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            await _categoryService.CreateAsync(request);
            return Ok("Category created successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            await _categoryService.UpdateAsync(id, request);
            return Ok("Category updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok("Category deleted successfully");
        }
    }
}
