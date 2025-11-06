using EmployeePayroll.Api.Models.Shared;

namespace EmployeePayroll.Api.Models.Employees
{
    public class CreateEmployeeResponse: BaseSpResponseModel
    {
        public int? EmployeeId { get; set; }
    }
}
