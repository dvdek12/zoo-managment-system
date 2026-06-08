using FluentValidation;
using ZooManagmentSystem.DTOs.Employee;

namespace ZooManagmentSystem.Validators
{
    public class EmployeeRegisterValidator : AbstractValidator<EmployeeRegisterDto>
    {
        public EmployeeRegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.BirthDay).LessThan(DateTime.Today);
        }
    }
}
