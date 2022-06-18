using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Controllers
{
    /// <summary>
    /// User controller class.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IUserService _userService;
        private readonly IConfirmEmailService _confirmEmailService;
        private readonly IResetPasswordService _resetPasswordService;
        public UserController(IUserService userService, IConfirmEmailService confirmEmailService, IResetPasswordService resetPasswordService)
        {
            _userService = userService;
            _confirmEmailService = confirmEmailService;
            _resetPasswordService = resetPasswordService;
        }

        /// <summary>
        /// Returns user profile
        /// </summary>
        /// <response code="200">Getting profile completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProfileResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _userService.GetProfileAsync(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="request">Profile data</param>
        /// <response code="200">Profile update completed successfully</response>
        /// <response code="400">Duplicate username</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            await _userService.UpdateProfileAsync(UserId, request);
            return Ok("Profile updated successfully");
        }

        /// <summary>
        /// Sends an email for confirmation
        /// </summary>
        /// <response code="200">Sending confirmation email successfully completed</response>
        /// <response code="400">Email sending failed</response>
        /// <response code="401">You are not authorized or email already confirmed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> SendConfirmEmail()
        {
            await _confirmEmailService.SendConfirmMailAsync(UserId);
            return Ok("Send email for confirmation");
        }

        /// <summary>
        /// Confirm email
        /// </summary>
        /// <param name="request">Email verification data</param>
        /// <response code="200">Email verification completed successfully</response>
        /// <response code="400">Invalid confirm token</response>
        /// <response code="401">Email already confirmed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            await _confirmEmailService.ConfirmEmailAsync(request);
            return Ok("Email confirmed");
        }

        /// <summary>
        /// Checking if email is confirmed
        /// </summary>
        /// <response code="200">Checking completed successfully</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet("IsEmailConfirmed")]
        public async Task<IActionResult> IsEmailConfirmed()
        {
            var result = await _confirmEmailService.IsEmailConfirmed(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Sends a password reset email
        /// </summary>
        /// <param name="request">Email</param>
        /// <response code="200">Sending a password reset request successfully completed</response>
        /// <response code="400">Email sending failed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> SendResetPassword([FromBody] EmailRequest request)
        {
            await _resetPasswordService.SendResetPasswordAsync(request);
            return Ok("Send email for reset password");
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="request">Reset password data</param>
        /// <response code="200">Reset password completed successfully</response>
        /// <response code="400">Invalid reset password token</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            await _resetPasswordService.ResetPasswordAsync(request);
            return Ok("Password reset successfully");
        }
    }
}
