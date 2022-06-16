using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            await _productService.CreateAsync(request);
            return Ok("Product created successfully");
        }

        //[Authorize(Roles = "Admin,Seller")]
        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest request)
        //{
        //    await _productService.UpdateAsync(id, request);
        //    return Ok("Product updated successfully");
        //}

        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("Product deleted successfully");
        }
    }
}
