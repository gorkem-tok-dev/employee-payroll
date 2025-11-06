using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.Shared;

namespace EmployeePayroll.Api.Data.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<CreateEmployeeResponse> CreateAsync(CreateEmployeeRequest request);
        Task<BaseSpResponseModel> UpdateAsync(UpdateEmployeeRequest request);
        Task<EmployeeDetailResponse?> GetDetailAsync(GetEmployeeDetailRequest model);
        Task<PagedEmployeesResponse> GetPagedAsync(GetEmployeesPagedRequest model);
    }
}
