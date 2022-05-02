using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Intefaces;
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
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _authService.SignInAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var result = await _authService.SignUpAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }

        [HttpPost("RefreshAccessToken")]
        public async Task<IActionResult> RefreshAccessToken(TokenRequest request)
        {
            var response = await _authService.RefreshAccessTokenAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(TokenRequest request)
        {
            await _authService.RevokeToken(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok("Logout success");
        }
    }
}

