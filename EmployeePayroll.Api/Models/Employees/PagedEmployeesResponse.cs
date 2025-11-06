namespace EmployeePayroll.Api.Models.Employees
{
    public class PagedEmployeesResponse
    {
        public List<EmployeeListItem> Employees { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class EmployeeListItem {             
            public int EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string IdentityNumber { get; set; } 
            public string EmployeeType { get; set; }
            public decimal Salary { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}
