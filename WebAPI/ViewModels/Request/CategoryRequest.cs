using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Category class to create and update category
    /// </summary>
    public class CategoryRequest
    {
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Technology and electronics </example>
        public string Name { get; set; }
        /// <summary>
        /// Category image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Parent Category identifier
        /// </summary>
        /// <example>1</example>
        public int? ParentId { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="CategoryRequest" /> validation
    /// </summary>
    public class CategoryRequestValidation : AbstractValidator<CategoryRequest>
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryRequestValidation(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
                   .Must(IsUniqueName).WithMessage("Category with this {PropertyName} already exists")
                   .Length(2, 50).WithMessage("{PropertyName} should be between 2 and 50 characters");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new CategoryGetByNameSpecification(name);
            return _categoryRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
