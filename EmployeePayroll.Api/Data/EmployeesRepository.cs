using Dapper;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.Shared;
using System.Data;

namespace EmployeePayroll.Api.Data
{
    public class EmployeesRepository:IEmployeesRepository
    {
        private readonly DapperContext _context;

        public EmployeesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<CreateEmployeeResponse> CreateAsync(CreateEmployeeRequest request)
        {
            using (var connection = _context.CreateConnection())
            {

                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", request.FirstName);
                parameters.Add("@LastName", request.LastName);
                parameters.Add("@IdentityNumber", request.IdentityNumber);
                parameters.Add("@EmployeeTypeId", request.EmployeeTypeId);
                parameters.Add("@Salary", request.Salary);

                var result = await connection.QueryFirstOrDefaultAsync<CreateEmployeeResponse>(
                    "sp_CreateEmployee",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<BaseSpResponseModel> UpdateAsync(UpdateEmployeeRequest request)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@FirstName", request.FirstName);
            parameters.Add("@LastName", request.LastName);
            parameters.Add("@IdentityNumber", request.IdentityNumber);
            parameters.Add("@EmployeeTypeId", request.EmployeeTypeId);
            parameters.Add("@Salary", request.Salary);

            var result = await connection.QueryFirstOrDefaultAsync<BaseSpResponseModel>(
                "sp_UpdateEmployee",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<EmployeeDetailResponse?> GetDetailAsync(GetEmployeeDetailRequest model)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", model.EmployeeId);

            var result = await connection.QueryFirstOrDefaultAsync<EmployeeDetailResponse>(
                "sp_GetEmployeeDetail",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<PagedEmployeesResponse> GetPagedAsync(GetEmployeesPagedRequest model)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", model.PageNumber);
            parameters.Add("@PageSize", model.PageSize);

            var result = await connection.QueryMultipleAsync(
                "sp_GetEmployeesPaged",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var employees = await result.ReadAsync<PagedEmployeesResponse.EmployeeListItem>();

            return new PagedEmployeesResponse
            {
                Employees = employees.ToList(),
                PageNumber = model.PageNumber,
                PageSize = model.PageSize
            };
        }
    }
}
