namespace EmployeePayroll.Api.Models.OvertimeEntries
{
    public class OvertimeEntriesHistoryResponse
    {
        public int OvertimeEntryId { get; set; }
        public DateTime WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
