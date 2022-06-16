using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
                   .EmailAddress().WithMessage("Invalid format of {PropertyName}");

            //Password
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Password").WithMessage("{PropertyName} is required");
        }
    }
}
