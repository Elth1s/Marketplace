using DAL;
using DAL.Entities;
using FluentValidation;
using WebAPI.Specifications.Filters;

namespace WebAPI.ViewModels.Request.Filters
{
    /// <summary>
    /// Filter group class to create and update filter group
    /// </summary>
    public class FilterGroupRequest
    {
        /// <summary>
        /// Name of filter group
        /// </summary>
        /// <example>Main</example>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="FilterGroupRequest" /> validation
    /// </summary>
    public class FilterGroupRequestValidator : AbstractValidator<FilterGroupRequest>
    {
        private readonly IRepository<FilterGroup> _filterGroupRepository;
        public FilterGroupRequestValidator(IRepository<FilterGroup> filterGroupRepository)
        {
            _filterGroupRepository = filterGroupRepository;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
               .Must(IsUniqueName).WithMessage("Filter group with this {PropertyName} already exists")
               .Length(2, 30).WithMessage("{PropertyName} should be between 2 and 30 characters");
        }

        private bool IsUniqueName(string name)
        {
            var spec = new FilterGroupGetByNameSpecification(name);
            return _filterGroupRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
