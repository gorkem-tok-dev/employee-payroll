using EmployeePayroll.Api.Models.Employees;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.Employees
{
    public class CreateEmployeeRequestValidator: AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(150).WithMessage("First name must not exceed 150 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 150 characters.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Identity number is required.")
                .Matches("^[0-9]*$").WithMessage("Identity number must contain only digits.")
                .Length(11).WithMessage("Identity number must be 11 characters.");

            RuleFor(x => x.EmployeeTypeId)
                .InclusiveBetween(1, 3).WithMessage("EmployeeTypeId must be 1 (Fixed Salary), 2 (Daily Wage), or 3 (Fixed + Overtime).");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive value.");
        }
    }
}
