using Dapper;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Payrolls;
using System.Data;

namespace EmployeePayroll.Api.Data
{
    public class PayrollsRepository : IPayrollsRepository
    {
        private readonly DapperContext _context;
        
        public PayrollsRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<CalculatePayrollResponse> CalculatePayroll(CalculatePayrollRequest request)
        {
            var dateOfMonth = new DateTime(request.Year, request.Month, 1);

            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@DateOfMonth", dateOfMonth);

            var result = await connection.QueryFirstOrDefaultAsync<CalculatePayrollResponse>(
                "sp_CalculatePayroll",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<List<GetPayrollReportResponse>> GetPayrollReport(DateTime dateOfMonth)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@DateOfMonth", dateOfMonth);

            var result = await connection.QueryMultipleAsync(
                "sp_GetPayrollReport",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summaries = await result.ReadAsync<GetPayrollReportResponse>();
            return summaries.ToList();
        }
    }
}
