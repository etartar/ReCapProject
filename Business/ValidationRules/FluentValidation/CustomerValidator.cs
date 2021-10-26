using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(c => c.CompanyName).MinimumLength(2).WithMessage(Messages.CustomerCompanyNameMinLength);
            RuleFor(c => c.CompanyName).MaximumLength(255).WithMessage(Messages.CustomerCompanyNameMaxLength);
        }
    }
}
