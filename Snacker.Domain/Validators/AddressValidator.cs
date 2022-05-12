using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("Please enter the CEP.")
                .NotNull().WithMessage("Please enter the CEP.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Please enter the street.")
                .NotNull().WithMessage("Please enter the street.");

            RuleFor(c => c.District)
                .NotEmpty().WithMessage("Please enter the district.")
                .NotNull().WithMessage("Please enter the district.");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("Please enter the city.")
                .NotNull().WithMessage("Please enter the city.");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("Please enter the address number.")
                .NotNull().WithMessage("Please enter the address number.");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("Please enter the country.")
                .NotNull().WithMessage("Please enter the country.");
        }
    }
}
