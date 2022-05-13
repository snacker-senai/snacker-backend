using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class RestaurantValidator : AbstractValidator<Restaurant>
    {
        public RestaurantValidator()
        {
            RuleFor(c => c.Name)
                            .NotEmpty().WithMessage("Please enter the name.")
                            .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Description)
                           .NotEmpty().WithMessage("Please enter the description.")
                           .NotNull().WithMessage("Please enter the description.");

            RuleFor(c => c.Address)
                           .NotEmpty().WithMessage("Please enter the address.")
                           .NotNull().WithMessage("Please enter the address.");

            RuleFor(c => c.RestaurantCategoryId)
                           .NotEmpty().WithMessage("Please enter the category.")
                           .NotNull().WithMessage("Please enter the category.");
        }
    }
}
