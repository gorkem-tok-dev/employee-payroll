namespace EmployeePayroll.Api.Models.OvertimeEntries
{
    public class GetOvertimeEntryHistoriesRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int EmployeeId { get; set; }
    }
}
