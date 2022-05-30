using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators.Product
{
    public class ProductStatusRequestValidator : AbstractValidator<ProductStatusRequest>
    {
        public ProductStatusRequestValidator()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 20).WithMessage("{PropertyName} should be between 2 and 20 characters");
        }
    }
}
