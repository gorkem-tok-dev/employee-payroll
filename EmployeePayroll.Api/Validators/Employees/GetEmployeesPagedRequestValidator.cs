using EmployeePayroll.Api.Models.Employees;
using FluentValidation;

namespace EmployeePayroll.Api.Validators.Employees
{
    public class GetEmployeesPagedRequestValidator:AbstractValidator<GetEmployeesPagedRequest>
    {
        public GetEmployeesPagedRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("PageNumber must be greater than 0. Paging starts from 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100 records per page.");
        }
    }
}
