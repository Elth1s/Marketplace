using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Sale class to create sale 
    /// </summary>
    public class SaleRequest
    {
        /// <summary>
        /// Name of sale
        /// </summary>
        /// <example>Sale Clothes</example>
        public string Name { get; set; }

        /// <summary>
        /// Ukrainian horizontal image of sale
        /// </summary>
        /// <example>https://some_horizontal_image_example.jpg</example>
        public string UkrainianHorizontalImage { get; set; }
        /// <summary>
        /// Ukrainian vertical image of sale
        /// </summary>
        /// <example>https://some_vertical_image_example.jpg</example>
        public string UkrainianVerticalImage { get; set; }
        /// <summary>
        /// English horizontal image of sale
        /// </summary>
        /// <example>https://some_horizontal_image_example.jpg</example>
        public string EnglishHorizontalImage { get; set; }
        /// <summary>
        /// English vertical image of sale
        /// </summary>
        /// <example>https://some_vertical_image_example.jpg</example>
        public string EnglishVerticalImage { get; set; }

        /// <summary>
        /// Discount Min of sale
        /// </summary>
        /// <example>1</example>
        public int DiscountMin { get; set; }
        /// <summary>
        /// Discount Max of sale
        /// </summary>
        /// <example>99</example>
        public int DiscountMax { get; set; }

        /// <summary>
        /// Date Start of sale
        /// </summary>
        /// <example>2022-09-22T18:18:18Z</example>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Date End of sale
        /// </summary>
        /// <example>2022-09-23T18:18:18Z</example>
        public DateTime DateEnd { get; set; }

        public IEnumerable<int> Categories { get; set; }
    }


    /// <summary>
    /// Class for <seealso cref="SaleRequest" /> validation
    /// </summary>
    public class SaleRequestValidator : AbstractValidator<SaleRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public SaleRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;

            //Name
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotEmpty().WithName(_validationResources["NamePropName"])
               .Length(2, 90);

            //DiscountMin
            RuleFor(a => a.DiscountMin).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DiscountMinPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(1, 99);

            //DiscountMax
            RuleFor(a => a.DiscountMax).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DiscountMaxPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(1, 99)
                .GreaterThan(a => a.DiscountMin).WithMessage(_validationResources["GreaterThanMessage"]);

            //DateStart
            RuleFor(a => a.DateStart).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DateStartPropName"]).WithMessage(_validationResources["RequiredMessage"]);

            //DateEnd
            RuleFor(a => a.DateEnd).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DateEndPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .GreaterThan(a => a.DateStart.Date).WithMessage(_validationResources["GreaterThanMessage"]);
        }
    }



}
