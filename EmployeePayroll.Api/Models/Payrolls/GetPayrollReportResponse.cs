namespace EmployeePayroll.Api.Models.Payrolls
{
    public class GetPayrollReportResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeType { get; set; }
        public int DaysWorked { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
