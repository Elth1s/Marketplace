using FluentValidation;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators.Product
{
    public class ProductRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 20).WithMessage("{PropertyName} should be between 2 and 20 characters");

            //Description
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName("Description").WithMessage("{PropertyName} is required")
              .Length(15, 150).WithMessage("{PropertyName} should be between 15 and 150 characters");
        }
    }
}
