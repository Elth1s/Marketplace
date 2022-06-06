using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
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
