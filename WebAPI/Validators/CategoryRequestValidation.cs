using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class CategoryRequestValidation : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidation()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 50).WithMessage("{PropertyName} should be between 2 and 50 characters");
        }
    }
}
