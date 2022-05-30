using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            RuleFor(c => c.TableId)
                 .NotEmpty().WithMessage("Please enter the table.")
                 .NotNull().WithMessage("Please enter the table.");
        }
    }
}
