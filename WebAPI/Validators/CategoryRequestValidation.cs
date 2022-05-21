using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;

namespace WebAPI.Validators
{
    public class CategoryRequestValidation : AbstractValidator<CategoryRequest>
    {
        private readonly IRepository<Category> _categoryRequest;

        public CategoryRequestValidation(IRepository<Category> categoryRequest)
        {
            _categoryRequest = categoryRequest;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Length(2, 50).WithMessage("{PropertyName} should be between 2 and 50 characters")
               .Must(IsUniqueName).WithMessage("Name with this {PropertyName} already exists");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new CategoryGetByNameSpecification(name);
            return _categoryRequest.GetBySpecAsync(spec).Result == null;
        }
    }
}
