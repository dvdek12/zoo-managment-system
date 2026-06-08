using FluentValidation;
using ZooManagmentSystem.DTOs.Reports;

namespace ZooManagmentSystem.Validators
{
    public class ReportCreateValidator : AbstractValidator<ReportCreateDto>
    {
        public ReportCreateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        }
    }
}
