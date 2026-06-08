using FluentValidation;
using ZooManagmentSystem.DTOs;

namespace ZooManagmentSystem.Validators
{
    public class EnclosureCreateValidator : AbstractValidator<EnclosureDto>
    {
        public EnclosureCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
