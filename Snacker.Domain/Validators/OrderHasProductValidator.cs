using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class OrderHasProductValidator : AbstractValidator<OrderHasProduct>
    {
        public OrderHasProductValidator()
        {
            RuleFor(c => c.OrderId)
                 .NotEmpty().WithMessage("Please enter the order.")
                 .NotNull().WithMessage("Please enter the order.");

            RuleFor(c => c.Product)
                 .NotEmpty().WithMessage("Please enter the product.")
                 .NotNull().WithMessage("Please enter the product.");
        }
    }
}
