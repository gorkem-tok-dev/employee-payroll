namespace EmployeePayroll.Api.Models.WorkEntries
{
    public class WorkEntriesSummaryResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime FirstRecordDate { get; set; }
        public DateTime LastRecordDate { get; set; }
        public int TotalWorkDaysCount { get; set; }

    }
}
