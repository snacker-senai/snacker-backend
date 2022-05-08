using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class RestaurantValidator : AbstractValidator<Restaurant>
    {
        public RestaurantValidator()
        {
            RuleFor(c => c.Name)
                            .NotEmpty().WithMessage("Please enter the restaurant name.")
                            .NotNull().WithMessage("Please enter the restaurant name.");

            RuleFor(c => c.Description)
                           .NotEmpty().WithMessage("Please enter the restaurant description.")
                           .NotNull().WithMessage("Please enter the restaurant description.");

            RuleFor(c => c.AddressId)
                           .NotEmpty().WithMessage("Please enter the restaurant address.")
                           .NotNull().WithMessage("Please enter the restaurant address.");

            RuleFor(c => c.RestaurantCategoryId)
                           .NotEmpty().WithMessage("Please enter the restaurant category.")
                           .NotNull().WithMessage("Please enter the restaurant category.");
        }
    }
}
