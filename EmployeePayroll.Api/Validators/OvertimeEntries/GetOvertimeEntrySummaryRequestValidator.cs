using EmployeePayroll.Api.Models.OvertimeEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.OvertimeEntries
{
    public class GetOvertimeEntrySummaryRequestValidator:AbstractValidator<GetOvertimeEntrySummaryRequest>
    {
        public GetOvertimeEntrySummaryRequestValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.Today.Year).WithMessage($"Year must be between 2000 and {DateTime.Today.Year}.");

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
        }
    }
}
