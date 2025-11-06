using EmployeePayroll.Api.Models.Payrolls;
using EmployeePayroll.Api.Models.Shared;
using EmployeePayroll.Api.Models.WorkEntries;

namespace EmployeePayroll.Api.Data.Interfaces
{
    public interface IPayrollsRepository
    {
        Task<CalculatePayrollResponse> CalculatePayroll(CalculatePayrollRequest request);
        Task<List<GetPayrollReportResponse>> GetPayrollReport(DateTime dateOfMonth);
    }
}
