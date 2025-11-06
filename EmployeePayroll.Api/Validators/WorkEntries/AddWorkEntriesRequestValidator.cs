using EmployeePayroll.Api.Models.WorkEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.WorkEntries
{
    public class AddWorkEntriesRequestValidator:AbstractValidator<AddWorkEntriesRequest>
    {
        public AddWorkEntriesRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.WorkDate)
                .LessThan(DateTime.Today.AddDays(1))
                .WithMessage("WorkDate cannot be in the future.");
        }
    }
}
