using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class RestaurantCategoryValidator : AbstractValidator<RestaurantCategory>
    {
        public RestaurantCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");
        }
    }
}
