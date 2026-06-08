using FluentValidation;
using ZooManagmentSystem.DTOs;

namespace ZooManagmentSystem.Validators
{
    public class TicketNewValidator : AbstractValidator<TicketNewDto>
    {
        public TicketNewValidator()
        {
            RuleFor(x => x.EntryTypeIds).NotEmpty();
        }
    }
}
