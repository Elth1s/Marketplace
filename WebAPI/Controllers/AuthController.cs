using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var result = await _authService.SignInAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var result = await _authService.SignUpAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] TokenRequest request)
        {
            await _authService.RevokeTokenAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok("Logout success");
        }


        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest request)
        {
            var result = await _authService.ExternalLoginAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }
    }
}

