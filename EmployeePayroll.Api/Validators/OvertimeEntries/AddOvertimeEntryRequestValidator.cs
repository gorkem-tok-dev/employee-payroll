using EmployeePayroll.Api.Models.OvertimeEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.OvertimeEntries
{
    public class AddOvertimeEntryRequestValidator:AbstractValidator<AddOvertimeEntryRequest>
    {
        public AddOvertimeEntryRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.WorkDate)
                .LessThan(DateTime.Today.AddDays(1)).WithMessage("WorkDate cannot be in the future.");

            RuleFor(x => x.Hours)
                .GreaterThan(0).WithMessage("Hours must be greater than 0.")
                .LessThanOrEqualTo(24).WithMessage("Hours cannot exceed 24.");
        }
    }
}
