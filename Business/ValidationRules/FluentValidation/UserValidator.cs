using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage(Messages.UserFirstNameMinLength);
            RuleFor(x => x.FirstName).MaximumLength(255).WithMessage(Messages.UserFirstNameMaxLength);
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage(Messages.UserLastNameMinLength);
            RuleFor(x => x.LastName).MaximumLength(255).WithMessage(Messages.UserLastNameMaxLength);
            RuleFor(x => x.PasswordHash).NotEmpty();
            RuleFor(x => x.PasswordSalt).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
