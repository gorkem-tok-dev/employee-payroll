using EmployeePayroll.Api.Models.Employees;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.Employees
{
    public class GetEmployeeDetailRequestValidator:AbstractValidator<GetEmployeeDetailRequest>
    {
        public GetEmployeeDetailRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
               .GreaterThan(0)
               .WithMessage("EmployeeId must be greater than 0.");
        }
    }
}
