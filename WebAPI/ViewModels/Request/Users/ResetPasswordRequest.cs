using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Reset password class to reset password for user
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// User identifier
        /// </summary>
        /// <example>94ddd073</example>
        public string UserId { get; set; }
        /// <summary>
        /// Token for reset password
        /// </summary>
        /// <example>some_reset_token</example>
        public string Token { get; set; }
        /// <summary>
        /// New password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
        /// <summary>
        /// Confirm password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ResetPasswordRequest" /> validation
    /// </summary>
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
           .NotEmpty().WithName("Password").WithMessage("{PropertyName} is required")
           .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one lowercase letter")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one uppercase letter")
           .Matches(@"(?=.*?[0-9])").WithMessage("{PropertyName} must contain at least one digit")
           .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage("{PropertyName} must contain at least one special character");


            //ConfirmPassword
            RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Confirm Password").WithMessage("{PropertyName} is required")
               .Equal(x => x.Password).WithMessage("Password and {PropertyName} do not match");
        }
    }
}
