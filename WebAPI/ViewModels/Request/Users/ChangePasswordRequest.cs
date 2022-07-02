using FluentValidation;

namespace WebAPI.ViewModels.Request.Users
{
    /// <summary>
    /// Change password class to change password for user
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// User old password
        /// </summary>
        /// <example>QWE_rty123</example>
        public string OldPassword { get; set; }
        /// <summary>
        /// User new password
        /// </summary>
        /// <example>321ytr_EWQ</example>
        public string Password { get; set; }
        /// <summary>
        /// User confirm new password
        /// </summary>
        /// <example>321ytr_EWQ</example>
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ChangePasswordRequest" /> validation
    /// </summary>
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            //OldPassword
            RuleFor(x => x.OldPassword).Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage("{PropertyName} is required");

            //Password
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one lowercase letter")
           .Matches(@"(?=.*[A-Z])").WithMessage("{PropertyName} must contain at least one uppercase letter")
           .Matches(@"(?=.*?[0-9])").WithMessage("{PropertyName} must contain at least one digit")
           .Matches(@"(?=.*?[!@#\$&*~_-])").WithMessage("{PropertyName} must contain at least one special character");

            //Confirm Password
            RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
           .NotEmpty().WithName("Confirm Password").WithMessage("{PropertyName} is required")
           .Equal(x => x.Password).WithMessage("Password and {PropertyName} do not match");
        }
    }

}
