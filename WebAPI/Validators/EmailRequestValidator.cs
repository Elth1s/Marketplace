using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Email address").WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Invalid format of {PropertyName}"); ;
        }
    }
}
