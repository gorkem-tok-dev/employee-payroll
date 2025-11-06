namespace EmployeePayroll.Api.Models.Employees
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public int EmployeeTypeId { get; set; }
        public decimal Salary { get; set; }
    }
}
