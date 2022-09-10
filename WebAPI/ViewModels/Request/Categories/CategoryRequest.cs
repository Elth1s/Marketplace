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
        /// English name of the category
        /// </summary>
        /// <example>Technology and electronics</example>
        public string EnglishName { get; set; }
        /// <summary>
        /// Ukrainian name of the category
        /// </summary>
        /// <example>Техніка та електроніка</example>
        public string UkrainianName { get; set; }
        /// <summary>
        /// Url of category
        /// </summary>
        /// <example>technology-and-electronics</example>
        public string UrlSlug { get; set; }
        /// <summary>
        /// Category image
        /// </summary>
        /// <example>https://some_category_image_example.jp</example>
        public string Image { get; set; }
        /// <summary>
        /// Category light icon
        /// </summary>
        /// <example>https://some_category_light_icon_example.jpg</example>
        public string LightIcon { get; set; }
        /// <summary>
        /// Category dark icon
        /// </summary>
        /// <example>https://some_category_dark_icon_example.jpg</example>
        public string DarkIcon { get; set; }
        /// <summary>
        /// Category active icon
        /// </summary>
        /// <example>https://some_category_active_icon_example.jpg</example>
        public string ActiveIcon { get; set; }
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
            //English Name
            RuleFor(x => x.EnglishName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["EnglishNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 40);
            //Ukrainian Name
            RuleFor(x => x.UkrainianName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["UkrainianNamePropName"]).WithMessage(_validationResources["RequiredMessage"])
               .Length(2, 40);
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
