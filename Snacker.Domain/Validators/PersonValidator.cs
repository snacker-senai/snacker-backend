using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("Please enter the birth date.")
                .NotNull().WithMessage("Please enter the person birth date.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Please enter the phone.")
                .NotNull().WithMessage("Please enter the phone.");

            RuleFor(c => c.RestaurantId)
                .NotEmpty().WithMessage("Please enter the restaurant.")
                .NotNull().WithMessage("Please enter the restaurant.");
        }
    }
}
