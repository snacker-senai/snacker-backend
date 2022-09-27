using FluentValidation;
using Snacker.Domain.Entities;

namespace Snacker.Domain.Validators
{
    public class ThemeValidator : AbstractValidator<Theme>
    {
        public ThemeValidator()
        {
            RuleFor(c => c.Color)
                .NotEmpty().WithMessage("Please enter the theme color.")
                .NotNull().WithMessage("Please enter the theme color.");
        }
    }
}
