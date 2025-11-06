using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeePayroll.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeesRepository _employeesRepository;
        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Yeni çalışan oluşturur",
            Description = "Yeni bir çalışan oluşturmak için bu servisi kullanabilirsiniz. T.C. Kimlik numarası benzersiz olmalıdır."
        )]
        [ProducesResponseType(typeof(CreateEmployeeResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateEmployeeRequest model)
        {
            var result = await _employeesRepository.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        [SwaggerOperation(
            Summary = "Çalışan kaydını günceller",
            Description = "Kayıtlı Çalışanın bilgilerini güncellemek için bu servisi kullanabilirsiniz."
        )]
        [ProducesResponseType(typeof(BaseSpResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmployeeRequest model)
        {
            var result = await _employeesRepository.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet("Detail")]
        [SwaggerOperation(
            Summary = "Çalışan detayını getirir",
            Description = "Kayıtlı Çalışların detayına ulaşmak için bu servisi kullanabilirsiniz."
        )]
        [ProducesResponseType(typeof(EmployeeDetailResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetailAsync([FromQuery] GetEmployeeDetailRequest model)
        {
            var result = await _employeesRepository.GetDetailAsync(model);
            return Ok(result);
        }

        [HttpGet("Paged")]
        [SwaggerOperation(
            Summary = "Çalışanların kayıtlarını listeler",
            Description = "Kayıtlı çalışanların listelenmesi için bu servisi kullanabilirsiniz. Sayfalama 1 den başlamalıdır."
        )]
        [ProducesResponseType(typeof(PagedEmployeesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetEmployeesPagedRequest model)
        {
            var result = await _employeesRepository.GetPagedAsync(model);
            return Ok(result);
        }
    }
}
