using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageCreateDtoValidator : AbstractValidator<CarImageCreateDto>
    {
        public CarImageCreateDtoValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.File).NotEmpty();
        }
    }
}
