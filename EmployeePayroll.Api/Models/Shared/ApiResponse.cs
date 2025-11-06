namespace EmployeePayroll.Api.Models.Shared
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
