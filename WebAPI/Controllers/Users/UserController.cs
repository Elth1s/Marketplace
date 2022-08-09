using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Interfaces.Users;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Users;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Users;

namespace WebAPI.Controllers.Users
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
        private readonly IConfirmPhoneService _confirmPhoneService;
        private readonly IStringLocalizer<UserController> _userLocalizer;

        public UserController(IUserService userService,
                              IConfirmEmailService confirmEmailService,
                              IResetPasswordService resetPasswordService,
                              IConfirmPhoneService confirmPhoneService,
                              IStringLocalizer<UserController> userLocalizer)
        {
            _userService = userService;
            _confirmEmailService = confirmEmailService;
            _resetPasswordService = resetPasswordService;
            _confirmPhoneService = confirmPhoneService;
            _userLocalizer = userLocalizer;
        }

        /// <summary>
        /// Return of sorted users
        /// </summary>
        /// <response code="200">Getting users completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdminSearchResponse<UserResponse>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchUsers([FromQuery] AdminSearchRequest request)
        {
            var result = await _userService.SearchUsersAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Delete an existing users
        /// </summary>
        /// <response code="200">Users deletion completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="403">You don't have permission</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUsers([FromQuery] IEnumerable<string> ids)
        {
            await _userService.DeleteUsersAsync(ids);
            return Ok(_userLocalizer["DeleteListSuccess"].Value);
        }

        #region Profile

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
            return Ok(_userLocalizer["UpdateProfileSuccess"].Value);
        }

        #endregion

        #region Email

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
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> SendConfirmEmail()
        {
            await _confirmEmailService.SendConfirmMailAsync(UserId);
            return Ok(_userLocalizer["SendConfirmEmailSuccess"].Value);
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
            return Ok(_userLocalizer["EmailConfirmSuccess"].Value);
        }

        /// <summary>
        /// Checking if email is confirmed
        /// </summary>
        /// <response code="200">Checking completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("IsEmailConfirmed")]
        public async Task<IActionResult> IsEmailConfirmed()
        {
            var result = await _confirmEmailService.IsEmailConfirmed(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Change user email
        /// </summary>
        /// <param name="request">Change email data</param>
        /// <response code="200">Email change completed successfully</response>
        /// <response code="400">Invalid password</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
        {
            await _userService.ChangeEmailAsync(UserId, request);
            return Ok(_userLocalizer["ChangeEmailSuccess"].Value);
        }

        #endregion

        #region Password

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
        [HttpPost("ResetPasswordByEmail")]
        public async Task<IActionResult> SendResetPasswordByEmail([FromBody] EmailRequest request)
        {
            await _resetPasswordService.SendResetPasswordByEmailAsync(request);
            return Ok(_userLocalizer["SendResetPasswordEmailSuccess"].Value);
        }


        /// <summary>
        /// Sends a password reset code to the phone
        /// </summary>
        /// <param name="request">Phone number</param>
        /// <response code="200">Sending a password reset request successfully completed</response>
        /// <response code="400">Phone sending failed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("ResetPasswordByPhone")]
        public async Task<IActionResult> SendResetPasswordByPhone([FromBody] PhoneRequest request)
        {
            await _resetPasswordService.SendResetPasswordByPhoneAsync(request);
            return Ok(_userLocalizer["SendResetPasswordPhoneCodeSuccess"].Value);
        }

        /// <summary>
        /// Validate phone password reset code
        /// </summary>
        /// <param name="request">Phone number and code</param>
        /// <response code="200">Validation successfully completed</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("ValidateResetPasswordPhoneCode")]
        public async Task<IActionResult> ValidateResetPasswordCodeForPhone([FromBody] CodeRequest request)
        {
            var result = await _resetPasswordService.ValidatePhoneCodeAsync(request);
            return Ok(result);
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
            return Ok(_userLocalizer["ResetPasswordSuccess"].Value);
        }


        /// <summary>
        /// Checking if user has password
        /// </summary>
        /// <response code="200">Checking completed successfully</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("HasPassword")]
        public async Task<IActionResult> HasPassword()
        {
            var result = await _userService.HasPassword(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <response code="200">Update completed successfully</response>
        /// <response code="400">Invalid old password or updating failed</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _userService.ChangePasswordAsync(UserId, request);
            return Ok(_userLocalizer["UpdatePasswordSuccess"].Value);
        }

        /// <summary>
        /// Add password for user
        /// </summary>
        /// <response code="200">Adding completed successfully</response>
        /// <response code="400">Password already exist or adding failed</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("AddPassword")]
        public async Task<IActionResult> AddPassword([FromBody] AddPasswordRequest request)
        {
            await _userService.AddPasswordAsync(UserId, request);
            return Ok(_userLocalizer["AddPasswordSuccess"].Value);
        }
        #endregion

        #region Phone

        /// <summary>
        /// Sends a code to your phone to verify your phone number
        /// </summary>
        /// <response code="200">Sending code successfully completed</response>
        /// <response code="400">Code sending failed</response>
        /// <response code="401">You are not authorized or phone already confirmed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("ConfirmPhoneNumber")]
        public async Task<IActionResult> SendVerificationCodeEmail()
        {
            await _confirmPhoneService.SendVerificationCodeAsync(UserId);
            return Ok(_userLocalizer["SendConfirmPhoneCodeSuccess"].Value);
        }

        /// <summary>
        /// Confirm phone number
        /// </summary>
        /// <param name="request">Phone verification data</param>
        /// <response code="200">Email verification completed successfully</response>
        /// <response code="400">Invalid code</response>
        /// <response code="401">You are not authorized or phone already confirmed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("ConfirmPhoneNumber")]
        public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmPhoneRequest request)
        {
            await _confirmPhoneService.VerificateAsync(UserId, request);
            return Ok(_userLocalizer["PhoneConfirmSuccess"].Value);
        }

        /// <summary>
        /// Checking if phone is confirmed
        /// </summary>
        /// <response code="200">Checking completed successfully</response>
        /// <response code="401">You are not authorized or email already confirmed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("IsPhoneConfirmed")]
        public async Task<IActionResult> IsPhoneConfirmed()
        {
            var result = await _confirmPhoneService.IsPhoneConfirmed(UserId);
            return Ok(result);
        }

        /// <summary>
        /// Change user phone
        /// </summary>
        /// <param name="request">Change phone data</param>
        /// <response code="200">Phone change completed successfully</response>
        /// <response code="400">Invalid password</response>
        /// <response code="401">You are not authorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An internal error has occurred</response>
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("ChangePhone")]
        public async Task<IActionResult> ChangePhone([FromBody] ChangePhoneRequest request)
        {
            await _userService.ChangePhoneAsync(UserId, request);
            return Ok(_userLocalizer["ChangePhoneSuccess"].Value);
        }

        #endregion
    }
}
