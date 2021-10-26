using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).WithMessage(Messages.ColorNameMinLength);
            RuleFor(x => x.Name).MaximumLength(255).WithMessage(Messages.ColorNameMaxLength);
        }
    }
}
