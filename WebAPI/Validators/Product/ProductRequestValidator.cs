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
              .Length(15, 250).WithMessage("{PropertyName} should be between 15 and 250 characters");

            //Price
            RuleFor(c => c.Price).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Price").WithMessage("{PropertyName} is required!")
               .InclusiveBetween(0.1f, float.MaxValue).WithMessage("{PropertyName} should be greater than 0.1");

            //Count
            RuleFor(a => a.Count).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName("Count").WithMessage("{PropertyName} is required!")
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} should be greater than 1");
        }
    }
}
