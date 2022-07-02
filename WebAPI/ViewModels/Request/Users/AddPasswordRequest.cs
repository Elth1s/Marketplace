using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Add password class to add password for user
    /// </summary>
    public class AddPasswordRequest
    {
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string Password { get; set; }
        /// <summary>
        /// User confirm password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="AddPasswordRequest" /> validation
    /// </summary>
    public class AddPasswordRequestValidator : AbstractValidator<AddPasswordRequest>
    {
        public AddPasswordRequestValidator()
        {
            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("{PropertyName} is required")
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
