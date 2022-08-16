using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Admin search class for searching, pagination and sorting
    /// </summary>
    public class AdminSearchRequest
    {
        /// <summary>
        /// Page
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; }

        /// <summary>
        /// Row per page
        /// </summary>
        /// <example>8</example>
        public int RowsPerPage { get; set; }

        /// <summary>
        /// Search string
        /// </summary>
        /// <example>some-string</example>
        public string Name { get; set; }

        /// <summary>
        /// Is ascending order
        /// </summary>
        /// <example>true</example>
        public bool IsAscOrder { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        /// <example>Id</example>
        public string OrderBy { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="AdminSearchRequest" /> validation
    /// </summary>
    public class AdminSearchRequestValidator : AbstractValidator<AdminSearchRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;

        public AdminSearchRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Page
            RuleFor(x => x.Page).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PagePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .GreaterThan(0).WithMessage(_validationResources["GreaterThanMessage"]);

            //RowsPerPage
            RuleFor(x => x.RowsPerPage).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["PagePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .GreaterThan(0).WithMessage(_validationResources["PluralGreaterThanMessage"]);

            //OrderBy
            RuleFor(x => x.OrderBy).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["OrderByPropName"]);
        }
    }
}
