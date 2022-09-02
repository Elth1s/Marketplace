using DAL.Entities;
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
        /// Image of sale
        /// </summary>
        /// <example>""</example>
        public string Image { get; set; }

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
        /// <example>12.09.2022</example>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Date End of sale
        /// </summary>
        /// <example>12.10.2022</example>
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
               .Length(2, 60);
            //DiscountMin
            RuleFor(a => a.DiscountMin).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DiscountMinPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(1, 99);
            //DiscountMax
            RuleFor(a => a.DiscountMax).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DiscountMaxPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(1, 99);
            //DateStart
            RuleFor(a => a.DateStart).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DateStartPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(DateTime.Now,DateTime.MaxValue);

            //DateEnd
            RuleFor(a => a.DateEnd).Cascade(CascadeMode.Stop)
                .NotEmpty().WithName(_validationResources["DateEndPropName"]).WithMessage(_validationResources["RequiredMessage"])
                .InclusiveBetween(DateTime.Now, DateTime.MaxValue);

        }
    }



}
