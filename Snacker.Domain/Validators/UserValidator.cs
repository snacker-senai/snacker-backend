using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Email)
                            .NotEmpty().WithMessage("Please enter the email.")
                            .NotNull().WithMessage("Please enter the email.");

            RuleFor(c => c.Password)
                           .NotEmpty().WithMessage("Please enter the password.")
                           .NotNull().WithMessage("Please enter the password.");

            RuleFor(c => c.UserTypeId)
                           .NotEmpty().WithMessage("Please enter the user type.")
                           .NotNull().WithMessage("Please enter the user type.");
        }
    }
}
