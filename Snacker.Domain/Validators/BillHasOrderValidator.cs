using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class BillHasOrderValidator : AbstractValidator<BillHasOrder>
    {
        public BillHasOrderValidator()
        {
            RuleFor(c => c.OrderId)
                 .NotEmpty().WithMessage("Please enter the order.")
                 .NotNull().WithMessage("Please enter the order.");

            RuleFor(c => c.BillId)
                 .NotEmpty().WithMessage("Please enter the bill.")
                 .NotNull().WithMessage("Please enter the bill.");
        }
    }
}
