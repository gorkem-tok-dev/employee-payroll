using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Payrolls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeePayroll.Api.Controllers
{
    [Route("api/payrolls")]
    [ApiController]
    public class PayrollsController : ControllerBase
    {
        IPayrollsRepository _payrollRepository;

        public PayrollsController(IPayrollsRepository payrollsRepository)
        {
            _payrollRepository = payrollsRepository;
        }

        [HttpPost("Calculate")]
        [SwaggerOperation(
            Summary = "Çalışanın maaşını hesaplar",
            Description = "Belirtilen çalışanın, seçilen ay için maaşını hesaplar. Hesaplama, çalışanın tipine göre (sabit maaş, günlük ücret veya sabit maaş + fazla mesai) yapılır. İşlem sonucunda hesaplanan maaş bilgileri döner."
        )]
        [ProducesResponseType(typeof(CalculatePayrollResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculatePayrollAsync([FromBody] CalculatePayrollRequest model)
        {
            var result = await _payrollRepository.CalculatePayroll(model);
            return Ok(result);
        }

        [HttpGet("Report")]
        [SwaggerOperation(
            Summary = "Aylık maaş raporunu getirir",
            Description = "Belirtilen yıl ve aya göre tüm çalışanların maaş hesaplama sonuçlarını döner. Raporlama ve kontrol amaçlı kullanılabilir."
        )]
        [ProducesResponseType(typeof(List<GetPayrollReportResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayrollReportAsync([FromQuery] GetPayrollReportRequest model)
        {
            var date = new DateTime(model.Year, model.Month, 1);
            var result = await _payrollRepository.GetPayrollReport(date);
            return Ok(result);
        }
    }
}
