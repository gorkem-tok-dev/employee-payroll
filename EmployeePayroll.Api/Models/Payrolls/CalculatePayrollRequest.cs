namespace EmployeePayroll.Api.Models.Payrolls
{
    public class CalculatePayrollRequest
    {
        public int EmployeeId { get;  set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
