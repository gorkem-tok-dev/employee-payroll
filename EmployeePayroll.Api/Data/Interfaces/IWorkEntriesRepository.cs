using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Shared;
using EmployeePayroll.Api.Models.WorkEntries;

namespace EmployeePayroll.Api.Data.Interfaces
{
    public interface IWorkEntriesRepository
    {
        Task<BaseSpResponseModel> AddAsync(AddWorkEntriesRequest request);
        Task<BaseSpResponseModel> DeleteAsync(DeleteWorkEntryRequest model);
        Task<List<WorkEntriesSummaryResponse>> SummaryAsync(DateTime dateOfMonth);
        Task<List<WorkEntriesHistoryResponse>> HistoryAsync(DateTime dateOfMonth, int employeeId);
    }
}
