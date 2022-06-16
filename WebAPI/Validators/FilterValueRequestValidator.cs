using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class FilterValueRequestValidator : AbstractValidator<FilterValueRequest>
    {
        public FilterValueRequestValidator()
        {
            //Value
            RuleFor(x => x.Value).Cascade(CascadeMode.Stop)
                //.NotEmpty().When(a => a.Min == null && a.Max == null).WithName("Value").WithMessage("{PropertyName} is required!")
                //.Empty().When(a => a.Max != null && a.Min != null).WithMessage("{PropertyName} must be empty!")
                .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");

            //Min
            RuleFor(a => a.Min).Cascade(CascadeMode.Stop);
            //.NotEmpty().When(a => a.Value == null || a.Max != null).WithName("Min").WithMessage("{PropertyName} is required!")
            //.Empty().When(a => a.Value != null).WithMessage("{PropertyName} must be empty!");

            //Max
            RuleFor(a => a.Max).Cascade(CascadeMode.Stop)
                //.NotEmpty().When(a => a.Value == null || a.Min != null).WithName("Max").WithMessage("{PropertyName} is required!")
                //.Empty().When(a => a.Value != null).WithMessage("{PropertyName} must be empty!")
                .GreaterThan(a => a.Min).When(a => a.Min != null).WithMessage("{PropertyName} must after min!");
        }
    }
}
