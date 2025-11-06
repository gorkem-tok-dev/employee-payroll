using EmployeePayroll.Api.Models.Payrolls;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.Payrolls
{
    public class CalculatePayrollRequestValidator: AbstractValidator<CalculatePayrollRequest>
    {
        public CalculatePayrollRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.Today.Year)
                .WithMessage($"Year must be between 2000 and {DateTime.Today.Year}.");

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12.");
        }
    }
}
