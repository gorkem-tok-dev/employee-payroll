using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Shared;

namespace EmployeePayroll.Api.Data.Interfaces
{
    public interface IOvertimeEntriesRepository
    {
        Task<BaseSpResponseModel> AddAsync(AddOvertimeEntryRequest request);
        Task<BaseSpResponseModel> UpdateAsync(UpdateOvertimeEntryRequest request);
        Task<BaseSpResponseModel> DeleteAsync(DeleteOvertimeEntryRequest model);
        Task<List<OvertimeEntriesSummaryResponse>> Summary(DateTime dateOfMonth);
        Task<List<OvertimeEntriesHistoryResponse>> GetHistoryAsync(DateTime dateOfMonth, int employeeId);
    }
}
