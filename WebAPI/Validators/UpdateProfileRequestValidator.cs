using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            //First name
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("First name").WithMessage("{PropertyName} is required")
               .Length(2, 15).WithMessage("{PropertyName} should be between 2 and 15 characters");

            //Second name
            RuleFor(x => x.SecondName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Second name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");

            //User name
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("User name").WithMessage("{PropertyName} is required")
              .Length(2, 40).WithMessage("{PropertyName} should be between 2 and 40 characters");
        }
    }
}
