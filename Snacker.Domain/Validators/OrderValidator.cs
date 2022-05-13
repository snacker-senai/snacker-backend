using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(c => c.TableId)
                 .NotEmpty().WithMessage("Please enter the table.")
                 .NotNull().WithMessage("Please enter the table.");
        }
    }
}
