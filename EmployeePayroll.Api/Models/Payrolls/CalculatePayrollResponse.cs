using EmployeePayroll.Api.Models.Shared;

namespace EmployeePayroll.Api.Models.Payrolls
{
    public class CalculatePayrollResponse:BaseSpResponseModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string EmployeeTypeName { get; set; }
        public int DaysWorked { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime CalculateDate { get; set; }

    }
}
