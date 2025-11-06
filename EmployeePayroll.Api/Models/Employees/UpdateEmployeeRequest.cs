namespace EmployeePayroll.Api.Models.Employees
{
    public class UpdateEmployeeRequest
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string IdentityNumber { get; set; }
        public string LastName { get; set; }
        public int EmployeeTypeId { get; set; }
        public decimal Salary { get; set; }
    }
}
