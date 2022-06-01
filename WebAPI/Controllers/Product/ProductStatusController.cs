using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels.Request;

namespace WebAPI.Interfaces
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : Controller
    {
        private readonly IProductStatusService _productStatusService;

        public ProductStatusController(IProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _productStatusService.GetAsync();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productStatusService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductStatusRequest request)
        {
            await _productStatusService.CreateAsync(request);
            return Ok("Product status updated successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductStatusRequest request)
        {
            await _productStatusService.UpdateAsync(id, request);
            return Ok("Product status updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productStatusService.DeleteAsync(id);
            return Ok("Product status deleted successfully");
        }
    }
}
