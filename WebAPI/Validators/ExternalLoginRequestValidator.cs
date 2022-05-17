using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class ExternalLoginRequestValidator : AbstractValidator<ExternalLoginRequest>
    {
        public ExternalLoginRequestValidator()
        {
            RuleFor(x => x.Provider).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Token).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
        }
    }
}
