using FluentValidation;

namespace WebAPI.ViewModels.Request
{
    /// <summary>
    /// Basket class to update 
    /// </summary>
    public class BasketUpdateRequest
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        /// <example>1</example>
        public int Count { get; set; }

    }


    /// <summary>
    /// Class for <seealso cref="BasketUpdateRequest" /> validation
    /// </summary>
    public class BasketUpdateRequestValidation : AbstractValidator<BasketUpdateRequest>
    {
        public BasketUpdateRequestValidation()
        {
            //Count
            RuleFor(a => a.Count).Cascade(CascadeMode.Stop)
                 .NotEmpty().WithName("Count").WithMessage("{PropertyName} is required!")
                 .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} should be greater than 1");
        }
    }



}
