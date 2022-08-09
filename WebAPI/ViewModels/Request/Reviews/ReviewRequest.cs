﻿using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Reviews
{
    /// <summary>
    /// Review class to create review 
    /// </summary>
    public class ReviewRequest
    {
        /// <summary>
        /// User full name
        /// </summary>
        /// <example>Nick Smith</example>
        public string FullName { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Product rating
        /// </summary>
        /// <example>4</example>
        public float ProductRating { get; set; }
        /// <summary>
        /// Product advantage
        /// </summary>
        /// <example>Some list of benefits</example>
        public string Advantage { get; set; }
        /// <summary>
        /// Product disadvantage
        /// </summary>
        /// <example>Some list of disadvantages</example>
        public string Disadvantage { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        /// <example>Some information</example>
        public string Comment { get; set; }
        /// <summary>
        /// Video URL
        /// </summary>
        /// <example>https://some_video_url_example.jpg</example>
        public string VideoURL { get; set; }
        /// <summary>
        /// Product slug
        /// </summary>
        /// <example>some-product-slug</example>
        public string ProductSlug { get; set; }
        /// <summary>
        /// List of images
        /// </summary>
        public ICollection<int> Images { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ReviewRequest" /> validation
    /// </summary>
    public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ReviewRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Full name
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["FullNamePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .Length(2, 80).WithMessage(_validationResources["PluralLengthMessage"]);

            //Email
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
              .NotEmpty().WithName(_validationResources["EmailAddressPropName"]).WithMessage(_validationResources["RequiredMessage"])
              .EmailAddress();

            //Product rating
            RuleFor(c => c.ProductRating).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["ProductRatingPropName"])
               .InclusiveBetween(1, 5);

            //Advantage
            RuleFor(a => a.Advantage).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["AdvantagePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .MaximumLength(100).WithMessage(_validationResources["PluralLengthMessage"]);

            //Disadvantage
            RuleFor(a => a.Disadvantage).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["DisadvantagePropName"]).WithMessage(_validationResources["PluralRequiredMessage"])
               .MaximumLength(100).WithMessage(_validationResources["PluralLengthMessage"]);

            //Comment
            RuleFor(a => a.Comment).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["CommentPropName"])
               .Length(1, 850);

            //Product slug 
            RuleFor(a => a.ProductSlug).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName(_validationResources["ProductUrlSlugPropName"])
                 .WithMessage(_validationResources["RequiredMessage"]);
        }
    }
}
