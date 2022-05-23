using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name)
                            .NotEmpty().WithMessage("Please enter the name.")
                            .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Description)
                           .NotEmpty().WithMessage("Please enter the description.")
                           .NotNull().WithMessage("Please enter the description.");

            RuleFor(c => c.Price)
                           .NotEmpty().WithMessage("Please enter the price.")
                           .NotNull().WithMessage("Please enter the price.");

            RuleFor(c => c.Image)
                           .NotEmpty().WithMessage("Please enter the image.")
                           .NotNull().WithMessage("Please enter the image.");

            RuleFor(c => c.ProductCategoryId)
                           .NotEmpty().WithMessage("Please enter the category.")
                           .NotNull().WithMessage("Please enter the category.");

            RuleFor(c => c.RestaurantId)
                           .NotEmpty().WithMessage("Please enter the restaurant.")
                           .NotNull().WithMessage("Please enter the restaurant.");
        }
    }
}
