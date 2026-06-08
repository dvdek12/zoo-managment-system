using FluentValidation;
using ZooManagmentSystem.DTOs.Animal;

namespace ZooManagmentSystem.Validators
{
    public class AnimalCreateValidator : AbstractValidator<AnimalCreateDto>
    {
        public AnimalCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RaceName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
