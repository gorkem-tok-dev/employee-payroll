using EmployeePayroll.Api.Models.OvertimeEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.OvertimeEntries
{
    public class DeleteOvertimeEntryRequestValidator:AbstractValidator<DeleteOvertimeEntryRequest>
    {
        public DeleteOvertimeEntryRequestValidator()
        {
            RuleFor(x => x.OvertimeId)
                .GreaterThan(0).WithMessage("OvertimeEntryId must be greater than 0.");
        }
    }
}
