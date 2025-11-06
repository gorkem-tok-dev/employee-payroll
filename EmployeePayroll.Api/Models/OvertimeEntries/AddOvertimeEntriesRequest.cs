namespace EmployeePayroll.Api.Models.OvertimeEntries
{
    public class AddOvertimeEntryRequest
    {
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
