using EmployeePayroll.Api.Data;
using EmployeePayroll.Api.Models.Payrolls;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeePayroll.Tests.IntegrationTests
{
    public class PayrollIntegrationTests
    {
        private readonly PayrollsRepository _repository;

        public PayrollIntegrationTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var context = new DapperContext(configuration);
            _repository = new PayrollsRepository(context);
        }

        /*------------------------------------------------
         |  Test Users in the Database
         -------------------------------------------------
         |  Employee Id | Type             | Base Salary
         -------------------------------------------------
         |  1005        | Fixed Salary     | 80000
         |  1006        | Daily Wage       | 2500
         |  1007        | Fixed + Overtime | 60000
         ------------------------------------------------*/

        [Fact]
        public async Task CalculatePayroll_ShouldReturn_ExpectedSalary_ForFixedSalary()
        {
            // Arrange
            var request = new CalculatePayrollRequest
            {
                EmployeeId = 1005,
                Month = 11,
                Year = 2025
            };

            // Act
            var result = await _repository.CalculatePayroll(request);

            // Assert
            result.Should().NotBeNull();
            result.TotalSalary.Should().Be(80000);
            result.BaseSalary.Should().Be(80000);
            result.EmployeeId.Should().Be(request.EmployeeId);
        }

        [Fact]
        public async Task CalculatePayroll_ShouldReturn_ExpectedSalary_ForDailyWage()
        {
            // Arrange
            var request = new CalculatePayrollRequest
            {
                EmployeeId = 1006,
                Month = 11,
                Year = 2025
            };

            // Act
            var result = await _repository.CalculatePayroll(request);

            // Assert
            result.Should().NotBeNull();
            result.BaseSalary.Should().Be(2500);
            result.TotalSalary.Should().Be(10000);
            result.EmployeeId.Should().Be(request.EmployeeId);
        }

        [Fact]
        public async Task CalculatePayroll_ShouldReturn_ExpectedSalary_ForDailyFixedAndOvertime()
        {
            // Arrange
            var request = new CalculatePayrollRequest
            {
                EmployeeId = 1007,
                Month = 11,
                Year = 2025 
            };

            // Act
            var result = await _repository.CalculatePayroll(request);

            // Assert
            result.Should().NotBeNull();
            result.BaseSalary.Should().Be(60000);
            result.TotalSalary.Should().Be(66000);
            result.EmployeeId.Should().Be(request.EmployeeId);
        }
    }
}
