namespace EmployeePayroll.Api.Models.WorkEntries
{
    public class GetWorkEntriesHistoryRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int EmployeeId { get; set; }
    }
}
