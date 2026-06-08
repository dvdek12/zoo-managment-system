using FluentValidation;
using ZooManagmentSystem.DTOs;

namespace ZooManagmentSystem.Validators
{
    public class ClientRegisterValidator : AbstractValidator<ClientRegisterDto>
    {
        public ClientRegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.BirthDay).LessThan(DateTime.Today);
        }
    }
}
