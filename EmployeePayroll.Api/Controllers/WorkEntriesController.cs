using EmployeePayroll.Api.Data;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.Payrolls;
using EmployeePayroll.Api.Models.Shared;
using EmployeePayroll.Api.Models.WorkEntries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeePayroll.Api.Controllers
{
    [Route("api/work-entries")]
    [ApiController]
    public class WorkEntriesController : ControllerBase
    {
        IWorkEntriesRepository _workEntriesRepository;
        public WorkEntriesController(IWorkEntriesRepository workEntriesRepository)
        {
            _workEntriesRepository = workEntriesRepository;
        }

        [HttpPost("Add")]
        [SwaggerOperation(
            Summary = "Yeni çalışma günü kaydı ekler",
            Description = "Belirtilen çalışana ait bir çalışma günü kaydı oluşturur. Aynı tarihe ait kayıt varsa yeni kayıt eklenmez."
        )]
        [ProducesResponseType(typeof(BaseSpResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] AddWorkEntriesRequest model)
        {
            var result = await _workEntriesRepository.AddAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Çalışma günü kaydını siler",
            Description = "Belirtilen ID değerine sahip çalışma günü kaydını siler. Eğer kayıt bulunamazsa hata mesajı döner."
        )]
        [ProducesResponseType(typeof(BaseSpResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteWorkEntryRequest model)
        {
            var result = await _workEntriesRepository.DeleteAsync(model);
            return Ok(result);
        }

        [HttpGet("summary")]
        [SwaggerOperation(
            Summary = "Aylık çalışma özetini getirir",
            Description = "Belirtilen yıl ve aya göre tüm çalışanların toplam çalışma günü sayılarını döner. Raporlama amaçlı kullanılabilir."
        )]
        [ProducesResponseType(typeof(List<WorkEntriesSummaryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummaryAsync([FromQuery] GetWorkEntriesSummaryRequest model)
        {
            var date = new DateTime(model.Year, model.Month, 1);
            var result = await _workEntriesRepository.SummaryAsync(date);
            return Ok(result);
        }

        [HttpGet("history")]
        [SwaggerOperation(
            Summary = "Çalışanın aylık çalışma geçmişini getirir",
            Description = "Belirtilen çalışanın, belirtilen ay içerisindeki çalışma kayıtlarını döner. Günlük bazda detaylı çalışma geçmişi sağlar."
        )]
        [ProducesResponseType(typeof(List<WorkEntriesHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistoryAsync([FromQuery] GetWorkEntriesHistoryRequest model)
        {
            var date = new DateTime(model.Year, model.Month, 1);
            var result = await _workEntriesRepository.HistoryAsync(date, model.EmployeeId);
            return Ok(result);
        }
    }
}
