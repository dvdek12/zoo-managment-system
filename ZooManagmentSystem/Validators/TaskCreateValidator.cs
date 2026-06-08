using FluentValidation;
using ZooManagmentSystem.DTOs.Employee;

namespace ZooManagmentSystem.Validators
{
    public class TaskCreateValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Deadline).GreaterThan(DateTime.Now);
        }
    }
}
