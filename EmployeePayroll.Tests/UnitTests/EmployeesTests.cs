using EmployeePayroll.Api.Controllers;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Employees;
using EmployeePayroll.Api.Models.Shared;
using EmployeePayroll.Api.Validators.Employees;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeePayroll.Tests.UnitTests
{
    public class EmployeesTests
    {
        private readonly Mock<IEmployeesRepository> _repoMock;
        private readonly EmployeesController _controller;
        private readonly CreateEmployeeRequestValidator _validator;

        public EmployeesTests()
        {
            _repoMock = new Mock<IEmployeesRepository>();
            _controller = new EmployeesController(_repoMock.Object);
            _validator = new CreateEmployeeRequestValidator();
        }

        // Create Employee Test
        [Fact]
        public async Task CreateAsync_ShouldReturnOk_WhenEmployeeCreatedSuccessfully()
        {
            // Arrange
            var request = new CreateEmployeeRequest
            {
                FirstName = "Görkem",
                LastName = "Tok",
                IdentityNumber = "12345678901",
                EmployeeTypeId = 1,
                Salary = 6000
            };

            var response = new CreateEmployeeResponse
            {
                IsSuccess = true,
                Message = "Employee created successfully",
                EmployeeId = 1
            };

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<CreateEmployeeRequest>()))
                     .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateEmployeeResponse>(okResult.Value);
            returnValue.IsSuccess.Should().BeTrue();
            returnValue.EmployeeId.Should().Be(1);
        }

        // Validator Tests for CreateEmployeeRequest
        [Fact]
        public void Should_Fail_When_FirstName_IsEmpty()
        {
            var model = new CreateEmployeeRequest
            {
                FirstName = "",
                LastName = "Tok",
                IdentityNumber = "12345678901",
                EmployeeTypeId = 1,
                Salary = 1000
            };

            var result = _validator.Validate(model);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "FirstName");
        }

        [Fact]
        public void Should_Fail_When_IdentityNumber_IsInvalid()
        {
            var model = new CreateEmployeeRequest
            {
                FirstName = "Görkem",
                LastName = "Tok",
                IdentityNumber = "123",
                EmployeeTypeId = 1,
                Salary = 1000
            };

            var result = _validator.Validate(model);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "IdentityNumber");
        }

        [Fact]
        public void Should_Pass_When_Model_IsValid()
        {
            var model = new CreateEmployeeRequest
            {
                FirstName = "Görkem",
                LastName = "Tok",
                IdentityNumber = "12345678901",
                EmployeeTypeId = 1,
                Salary = 65000
            };

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        // Update Employee Test
        [Fact]
        public async Task UpdateAsync_ShouldReturnOk_WhenEmployeeUpdatedSuccessfully()
        {
            // Arrange
            var request = new UpdateEmployeeRequest
            {
                EmployeeId = 1,
                FirstName = "Görkem",
                LastName = "Tok",
                Salary = 7000
            };

            var response = new BaseSpResponseModel
            {
                IsSuccess = true,
                Message = "Employee updated successfully"
            };

            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<UpdateEmployeeRequest>()))
                     .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaseSpResponseModel>(okResult.Value);
            returnValue.IsSuccess.Should().BeTrue();
            returnValue.Message.Should().Contain("updated");
        }

        // Get Employee Detail Test
        [Fact]
        public async Task GetDetailAsync_ShouldReturnEmployeeDetail_WhenEmployeeExists()
        {
            // Arrange
            var request = new GetEmployeeDetailRequest { EmployeeId = 1 };

            var employeeDetail = new EmployeeDetailResponse
            {
                EmployeeId = 1,
                FirstName = "Görkem",
                LastName = "Tok",
                IdentityNumber = "*******1111",
                EmployeeType = "Sabit Maaş + Mesai",
                CreatedAt = DateTime.Now.AddMonths(-2),
                UpdatedAt = DateTime.Now.AddMonths(-1),
                Salary = 6000
            };

            _repoMock.Setup(r => r.GetDetailAsync(It.IsAny<GetEmployeeDetailRequest>()))
                     .ReturnsAsync(employeeDetail);

            // Act
            var result = await _controller.GetDetailAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<EmployeeDetailResponse>(okResult.Value);
            returnValue.FirstName.Should().Be("Görkem");
            returnValue.EmployeeId.Should().Be(1);
            returnValue.EmployeeType.Should().Be("Sabit Maaş + Mesai");
        }

        // Get Paged Employees Test
        [Fact]
        public async Task GetPagedEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<PagedEmployeesResponse.EmployeeListItem>
            {
                new PagedEmployeesResponse.EmployeeListItem { EmployeeId = 1, FirstName = "Görkem", LastName ="Tok", EmployeeType = "1", CreatedAt = DateTime.Now,
                IdentityNumber = "*******1111", Salary = 1000},
                new PagedEmployeesResponse.EmployeeListItem { EmployeeId = 2, FirstName = "Patik", LastName ="Tok", EmployeeType = "2", CreatedAt = DateTime.Now,
                IdentityNumber = "*******1112", Salary = 2000},
                new PagedEmployeesResponse.EmployeeListItem { EmployeeId = 3, FirstName = "Bengü", LastName ="Girgin", EmployeeType = "3", CreatedAt = DateTime.Now,
                IdentityNumber = "*******1113", Salary = 2000},
            };

            var employeesResponse = new PagedEmployeesResponse
            {
                Employees = employees,
                PageNumber = 1,
                PageSize = 10
            };

            _repoMock.Setup(repo => repo.GetPagedAsync(It.IsAny<GetEmployeesPagedRequest>())).ReturnsAsync(employeesResponse);

            // Act
            var result = await _controller.GetPagedAsync(new GetEmployeesPagedRequest() { PageNumber = 1, PageSize = 10 });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<PagedEmployeesResponse>(okResult.Value);
            Assert.Equal(3, returnEmployees.Employees?.Count);
        }
    }
}
