using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
{
    /// <summary>
    /// User authentication controller class
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Sign in on the site
        /// </summary>
        /// <param name="request">User data</param>
        /// <response code="200">Sign in completed successfully</response>
        /// <response code="400">ReCaptcha validation failed</response>
        /// <response code="401">Invalid user email or password</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var result = await _authService.SignInAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }

        /// <summary>
        /// Sign up on the site
        /// </summary>
        /// <param name="request">New user data</param>
        /// <response code="200">Sign up completed successfully</response>
        /// <response code="400">ReCaptcha validation or creating user failed</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var result = await _authService.SignUpAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }

        /// <summary>
        /// Refresh access and refresh tokens
        /// </summary>
        /// <param name="request">Refresh token</param>
        /// <response code="200">Refresh tokens completed successfully</response>
        /// <response code="400">The refresh token is not active or invalid</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(response);
        }

        /// <summary>
        /// Logout of site
        /// </summary>
        /// <param name="request">Refresh token</param>
        /// <response code="200">Logout completed successfully</response>
        /// <response code="400">The refresh token is revoked, not active or invalid</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] TokenRequest request)
        {
            await _authService.RevokeTokenAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok("Logout success");
        }

        /// <summary>
        /// Authorization on the site with Google
        /// </summary>
        /// <param name="request">Provider and token for Google external login</param>
        /// <response code="200">Google external login completed successfully</response>
        /// <response code="400">Adding external login or creation user failed</response>
        /// <response code="500">Token validation failed or an internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest request)
        {
            var result = await _authService.ExternalLoginAsync(request, IpUtil.GetIpAddress(Request, HttpContext));
            return Ok(result);
        }
    }
}

