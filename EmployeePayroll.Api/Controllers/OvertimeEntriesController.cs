using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeePayroll.Api.Controllers
{
    [Route("api/overtime-entries")]
    [ApiController]
    public class OvertimeEntriesController : ControllerBase
    {
        IOvertimeEntriesRepository _overtimeEntriesRepository;

        public OvertimeEntriesController(IOvertimeEntriesRepository overtimeEntriesRepository)
        {
            _overtimeEntriesRepository = overtimeEntriesRepository;
        }
        
        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Yeni fazla mesai kaydı ekler",
            Description = "Belirtilen çalışana ait, belirtilen tarihteki fazla mesai kaydını oluşturur. Aynı güne ait bir kayıt varsa yeni kayıt oluşturulmaz."
        )]
        [ProducesResponseType(typeof(BaseSpResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] AddOvertimeEntryRequest request)
        {
            var result = await _overtimeEntriesRepository.AddAsync(request);
            return Ok(result);
        }


        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Fazla mesai kaydını günceller",
            Description = "Var olan fazla mesai kaydının tarih veya saat bilgilerini güncellemek için kullanılır."
        )]
        [ProducesResponseType(typeof(BaseSpResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOvertimeEntryRequest request)
        {
            var result = await _overtimeEntriesRepository.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Fazla mesai kaydını siler",
            Description = "Belirtilen ID değerine sahip fazla mesai kaydını veritabanından siler. Eğer kayıt bulunamazsa hata mesajı döner."
        )]
        [ProducesResponseType(typeof(BaseSpResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteOvertimeEntryRequest model)
        {
            var result = await _overtimeEntriesRepository.DeleteAsync(model);
            return Ok(result);
        }

        [HttpGet("summary")]
        [SwaggerOperation(
            Summary = "Fazla mesai özetini getirir",
            Description = "Belirtilen yıl ve aya göre tüm çalışanların fazla mesai toplam saatlerini döner. Raporlama amaçlı kullanılabilir."
        )]
        [ProducesResponseType(typeof(List<OvertimeEntriesSummaryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummaryAsync([FromQuery] GetOvertimeEntrySummaryRequest model)
        {
            var date = new DateTime(model.Year, model.Month, 1);
            var result = await _overtimeEntriesRepository.Summary(date);
            return Ok(result);
        }

        [HttpGet("history")]
        [SwaggerOperation(
            Summary = "Çalışanın fazla mesai geçmişini getirir",
            Description = "Belirtilen çalışanın, belirtilen yıl ve ay içindeki fazla mesai kayıtlarını döner."
        )]
        [ProducesResponseType(typeof(List<OvertimeEntriesHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistoryAsync([FromQuery] GetOvertimeEntryHistoriesRequest model)
        {
            var date = new DateTime(model.Year, model.Month, 1);
            var result = await _overtimeEntriesRepository.GetHistoryAsync(date, model.EmployeeId);
            return Ok(result);
        }
    }
}
