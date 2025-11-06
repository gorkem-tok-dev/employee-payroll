using EmployeePayroll.Api.Models.WorkEntries;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.WorkEntries
{
    public class GetWorkEntriesSummaryRequestValidator:AbstractValidator<GetWorkEntriesSummaryRequest>
    {
        public GetWorkEntriesSummaryRequestValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.Today.Year)
                .WithMessage($"Year must be between 2000 and {DateTime.Today.Year}.");

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12.");
        }
    }
}
