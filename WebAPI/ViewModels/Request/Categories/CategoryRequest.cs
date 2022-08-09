using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace WebAPI.ViewModels.Request.Categories
{
    /// <summary>
    /// Category class to create and update category
    /// </summary>
    public class CategoryRequest
    {
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Technology and electronics</example>
        public string Name { get; set; }
        /// <summary>
        /// Url of category
        /// </summary>
        /// <example>technology-and-electronics</example>
        public string UrlSlug { get; set; }
        /// <summary>
        /// Category image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Category icon
        /// </summary>
        public string Icon { get; set; }
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
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public CategoryRequestValidation(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName(_validationResources["NamePropName"])
                   .Length(2, 50);

            //UrlSlug
            RuleFor(x => x.UrlSlug).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName(_validationResources["CategoryUrlSlugPropName"]).WithMessage(_validationResources["RequiredMessage"])
                   .Must(IsValidUrlSlug).WithMessage(_validationResources["InvalidFormatMessage"])
                   .Length(2, 50);
        }

        private bool IsValidUrlSlug(string urlSlug)
        {
            return Regex.IsMatch(urlSlug, @"^[a-z0-9]+(?:-[a-z0-9]+)*$");

        }
    }
}
