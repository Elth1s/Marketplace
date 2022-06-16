using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;

namespace WebAPI.Controllers
{
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

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _userService.GetProfileAsync(UserId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            await _userService.UpdateProfileAsync(UserId, request);
            return Ok("Profile updated successfully");
        }

        [Authorize]
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> SendConfirmEmail()
        {

            await _confirmEmailService.SendConfirmMailAsync(UserId);
            return Ok("Send email for confirmation");
        }

        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {

            await _confirmEmailService.ConfirmEmailAsync(request);
            return Ok("Email confirmed");
        }

        [HttpGet("IsEmailConfirmed")]
        public async Task<IActionResult> IsEmailConfirmed()
        {

            var result = await _confirmEmailService.IsEmailConfirmed(UserId);
            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> SendResetPassword([FromBody] EmailRequest request)
        {

            await _resetPasswordService.SendResetPasswordAsync(request);
            return Ok("Send email for reset password");
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {

            await _resetPasswordService.ResetPasswordAsync(request);
            return Ok("Password reset successfully");
        }
    }
}
