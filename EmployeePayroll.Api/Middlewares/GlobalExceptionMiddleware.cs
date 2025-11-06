using EmployeePayroll.Api.Models.Shared;

namespace EmployeePayroll.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Kritik bir hata oluştu, bir süre bekleyin ve tekrar deneyin."
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
