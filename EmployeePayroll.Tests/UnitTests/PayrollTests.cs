using EmployeePayroll.Api.Controllers;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Payrolls;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeePayroll.Tests.UnitTests
{
    public class PayrollTests
    {
        private readonly Mock<IPayrollsRepository> _repoMock;
        private readonly PayrollsController _controller;

        public PayrollTests()
        {
            _repoMock = new Mock<IPayrollsRepository>();
            _controller = new PayrollsController(_repoMock.Object);
        }

        [Fact]
        public async Task CalculatePayrollAsync_Should_ReturnOk_When_ValidRequest()
        {
            // Arrange
            var request = new CalculatePayrollRequest
            {
                EmployeeId = 1,
                Month = 11,
                Year = 2025
            };

            var expectedResponse = new CalculatePayrollResponse
            {
                EmployeeId = 1,
                FirstName = "Görkem",
                LastName = "Tok",
                DaysWorked = 20,
                OvertimeHours = 5,
                BaseSalary = 4225,
                TotalSalary = 84500,
                IsSuccess = true
            };

            _repoMock.Setup(r => r.CalculatePayroll(It.IsAny<CalculatePayrollRequest>()))
                     .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CalculatePayrollAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CalculatePayrollResponse>(okResult.Value);

            returnValue.IsSuccess.Should().BeTrue();
            returnValue.TotalSalary.Should().Be(84500);
            returnValue.DaysWorked.Should().Be(20);
        }

        [Fact]
        public async Task GetPayrollReportAsync_Should_ReturnList_When_DataExists()
        {
            // Arrange
            var model = new GetPayrollReportRequest { Month = 11, Year = 2025 };

            var reportList = new List<GetPayrollReportResponse>
            {
                new GetPayrollReportResponse
                {
                    EmployeeId = 1,
                    FirstName = "Görkem",
                    LastName = "Tok",
                    DaysWorked = 20,
                    BaseSalary = 4225,
                    TotalSalary = 84500,
                    OvertimeHours = 0
                },
                new GetPayrollReportResponse
                {
                    EmployeeId = 2,
                    FirstName = "Patik",
                    LastName = "Tok",
                    DaysWorked = 10,
                    BaseSalary = 1000,
                    TotalSalary = 10000,
                    OvertimeHours = 0
                }
            };

            _repoMock.Setup(r => r.GetPayrollReport(It.IsAny<DateTime>()))
                     .ReturnsAsync(reportList);

            // Act
            var result = await _controller.GetPayrollReportAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<GetPayrollReportResponse>>(okResult.Value);

            returnValue.Should().HaveCount(2);
            returnValue[0].TotalSalary.Should().Be(84500);
            returnValue[1].DaysWorked.Should().Be(18);
        }
    }
}
