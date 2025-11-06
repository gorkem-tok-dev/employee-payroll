using EmployeePayroll.Api.Models.WorkEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.WorkEntries
{
    public class DeleteWorkEntryRequestValidator:AbstractValidator<DeleteWorkEntryRequest>
    {
        public DeleteWorkEntryRequestValidator()
        {
            RuleFor(x => x.WorkEntryId)
                .GreaterThan(0)
                .WithMessage("WorkEntryId must be greater than 0.");
        }
    }
}
