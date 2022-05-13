using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class UserTypeValidator : AbstractValidator<UserType>
    {
        public UserTypeValidator()
        {
            RuleFor(c => c.Name)
                 .NotEmpty().WithMessage("Please enter the name.")
                 .NotNull().WithMessage("Please enter the name.");
        }
    }
}
