namespace EmployeePayroll.Api.Models.OvertimeEntries
{
    public class UpdateOvertimeEntryRequest
    {
        public int OvertimeId { get;  set; }
        public DateTime WorkDate { get;  set; }
        public int Hours { get;  set; }
        public int EmployeeId { get;  set; }
    }
}
