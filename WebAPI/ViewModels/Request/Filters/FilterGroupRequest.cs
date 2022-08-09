using DAL;
using DAL.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;

        private readonly IRepository<FilterGroup> _filterGroupRepository;
        public FilterGroupRequestValidator(IRepository<FilterGroup> filterGroupRepository,
            IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            _filterGroupRepository = filterGroupRepository;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Must(IsUniqueName).WithMessage(_validationResources["FilterGroupUniqueNameMessage"])
               .Length(2, 30);
        }

        private bool IsUniqueName(string name)
        {
            var spec = new FilterGroupGetByNameSpecification(name);
            return _filterGroupRepository.GetBySpecAsync(spec).Result == null;
        }
    }
}
