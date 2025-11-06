namespace EmployeePayroll.Api.Models.WorkEntries
{
    public class AddWorkEntriesRequest
    {
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
    }
}
