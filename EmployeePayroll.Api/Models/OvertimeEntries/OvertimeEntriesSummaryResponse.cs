namespace EmployeePayroll.Api.Models.OvertimeEntries
{
    public class OvertimeEntriesSummaryResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime FirstRecordDate { get; set; }
        public DateTime LastRecordDate { get; set; }
        public int TotalHours { get; set; }
    }
}
