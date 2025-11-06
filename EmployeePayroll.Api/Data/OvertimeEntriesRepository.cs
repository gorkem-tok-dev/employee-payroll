using Dapper;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Shared;
using System.Data;

namespace EmployeePayroll.Api.Data
{
    public class OvertimeEntriesRepository : IOvertimeEntriesRepository
    {
        private readonly DapperContext _context;
        public OvertimeEntriesRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<BaseSpResponseModel> AddAsync(AddOvertimeEntryRequest request)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@WorkDate", request.WorkDate);
            parameters.Add("@Hours", request.Hours);

            var result = await connection.QueryFirstOrDefaultAsync<BaseSpResponseModel>(
                "sp_AddOvertimeEntry",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<BaseSpResponseModel> UpdateAsync(UpdateOvertimeEntryRequest request)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@OvertimeId", request.OvertimeId);
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@WorkDate", request.WorkDate);
            parameters.Add("@Hours", request.Hours);

            var result = await connection.QueryFirstOrDefaultAsync<BaseSpResponseModel>(
                "sp_UpdateOvertimeEntry",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<BaseSpResponseModel> DeleteAsync(DeleteOvertimeEntryRequest model)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@OvertimeId", model.OvertimeId);

            var result = await connection.QueryFirstOrDefaultAsync<BaseSpResponseModel>(
                "sp_DeleteOvertimeEntry",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<List<OvertimeEntriesSummaryResponse>> Summary(DateTime dateOfMonth)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@DateOfMonth", dateOfMonth);

            var result = await connection.QueryMultipleAsync(
                "sp_OvertimeSummary",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summaries = await result.ReadAsync<OvertimeEntriesSummaryResponse>();
            return summaries.ToList();
        }

        public async Task<List<OvertimeEntriesHistoryResponse>> GetHistoryAsync(DateTime dateOfMonth, int employeeId)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@DateOfMonth", dateOfMonth);
            parameters.Add("@EmployeeId", employeeId);

            var result = await connection.QueryMultipleAsync(
                "sp_OvertimeHistory",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summaries = await result.ReadAsync<OvertimeEntriesHistoryResponse>();
            return summaries.ToList();
        }
    }
}
