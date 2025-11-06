using Azure.Core;
using Dapper;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Shared;
using EmployeePayroll.Api.Models.WorkEntries;
using System.Data;

namespace EmployeePayroll.Api.Data
{
    public class WorkEntriesRepository : IWorkEntriesRepository
    {
        private readonly DapperContext _context;
        public WorkEntriesRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<BaseSpResponseModel> AddAsync(AddWorkEntriesRequest request)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@WorkDate", request.WorkDate);

            var result = await connection.QueryFirstOrDefaultAsync<BaseSpResponseModel>(
                "sp_AddWorkEntry",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<BaseSpResponseModel> DeleteAsync(DeleteWorkEntryRequest model)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@WorkEntryId", model.WorkEntryId);

            var result = await connection.QueryFirstOrDefaultAsync<BaseSpResponseModel>(
                "sp_DeleteWorkEntry",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<List<WorkEntriesSummaryResponse>> SummaryAsync(DateTime dateOfMonth)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@DateOfMonth", dateOfMonth);

            var result = await connection.QueryMultipleAsync(
                "sp_WorkEntriesSummary",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summaries = await result.ReadAsync<WorkEntriesSummaryResponse>();
            return summaries.ToList();
        }

        public async Task<List<WorkEntriesHistoryResponse>> HistoryAsync(DateTime dateOfMonth, int employeeId)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@DateOfMonth", dateOfMonth);
            parameters.Add("@EmployeeId", employeeId);

            var result = await connection.QueryMultipleAsync(
                "sp_WorkEntriesHistory",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summaries = await result.ReadAsync<WorkEntriesHistoryResponse>();
            return summaries.ToList();
        }

        
    }
}
