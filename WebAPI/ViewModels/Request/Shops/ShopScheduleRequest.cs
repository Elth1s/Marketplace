using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WebAPI.ViewModels.Request.Shops
{
    /// <summary>
    /// Shop schedule class to update shop schedule
    /// </summary>
    public class ShopScheduleRequest
    {
        public IEnumerable<ShopScheduleItemRequest> Items { get; set; }
    }


    public class ShopScheduleItemRequest
    {
        /// <summary>
        /// Day of week identifier
        /// </summary>
        /// <example>1</example>
        public int DayOfWeekId { get; set; }
        /// <summary>
        /// Time start
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// Time start
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// Is Weekend
        /// </summary>
        /// <example>false</example>
        public bool IsWeekend { get; set; }
    }

    /// <summary>
    /// Class for <seealso cref="ShopScheduleRequest" /> validation
    /// </summary>
    public class ShopScheduleRequestValidator : AbstractValidator<ShopScheduleRequest>
    {
        private readonly IStringLocalizer<ValidationResourсes> _validationResources;
        public ShopScheduleRequestValidator(IStringLocalizer<ValidationResourсes> validationResources)
        {
            _validationResources = validationResources;
            RuleForEach(s => s.Items)
                .ChildRules(child =>
                {
                    child.RuleFor(s => s.Start).Cascade(CascadeMode.Stop)
                .NotEmpty().When(s => !s.IsWeekend).WithName(_validationResources["TimeStartPropName"]);

                    child.RuleFor(s => s.End).Cascade(CascadeMode.Stop)
                .NotEmpty().When(s => !s.IsWeekend).WithName(_validationResources["TimeEndPropName"])
                .Must(
                (obj, s) => { return TimeOnly.FromDateTime(obj.Start) < TimeOnly.FromDateTime(s); })
                .When(s => !s.IsWeekend).WithMessage(_validationResources["EndGreaterStart"]);
                }).When(s => s.Items.Any());
        }
    }
}
