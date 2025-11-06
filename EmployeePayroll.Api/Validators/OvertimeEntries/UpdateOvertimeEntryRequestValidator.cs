using EmployeePayroll.Api.Models.OvertimeEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.OvertimeEntries
{
    public class UpdateOvertimeEntryRequestValidator:AbstractValidator<UpdateOvertimeEntryRequest>
    {
        public UpdateOvertimeEntryRequestValidator()
        {
            RuleFor(x => x.OvertimeId)
                .GreaterThan(0).WithMessage("OvertimeId must be greater than 0.");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.WorkDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("WorkDate cannot be in the future.");

            RuleFor(x => x.Hours)
                .GreaterThan(0).WithMessage("Hours must be greater than 0.")
                .LessThanOrEqualTo(24).WithMessage("Hours cannot exceed 24.");
        }
    }
}
