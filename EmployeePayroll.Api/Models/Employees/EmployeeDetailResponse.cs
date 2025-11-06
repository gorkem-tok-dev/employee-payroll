namespace EmployeePayroll.Api.Models.Employees
{
    public class EmployeeDetailResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public decimal Salary { get; set; }
        public string EmployeeType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
