using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageUpdateDtoValidator : AbstractValidator<CarImageUpdateDto>
    {
        public CarImageUpdateDtoValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.File).NotEmpty();
            RuleFor(x => x.ImagePath).NotEmpty();
        }
    }
}
