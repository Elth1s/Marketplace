using FluentValidation;
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

        public CategoryRequestValidation()
        {
            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName("Name").WithMessage("{PropertyName} is required")
                   .Length(2, 50).WithMessage("{PropertyName} should be between 2 and 50 characters");

            //UrlSlug
            RuleFor(x => x.UrlSlug).Cascade(CascadeMode.Stop)
                   .NotEmpty().WithName("Url slug").WithMessage("{PropertyName} is required")
                   .Must(IsValidUrlSlug).WithMessage("Invalid format of {PropertyName}")
                   .Length(2, 50).WithMessage("{PropertyName} should be between 2 and 50 characters");
        }

        private bool IsValidUrlSlug(string urlSlug)
        {
            return Regex.IsMatch(urlSlug, @"^[a-z0-9]+(?:-[a-z0-9]+)*$");

        }
    }
}
