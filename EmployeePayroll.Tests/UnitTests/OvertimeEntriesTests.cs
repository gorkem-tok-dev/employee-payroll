using EmployeePayroll.Api.Controllers;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.OvertimeEntries;
using EmployeePayroll.Api.Models.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeePayroll.Tests.UnitTests
{
    public class OvertimeEntriesTests
    {
        private readonly Mock<IOvertimeEntriesRepository> _repoMock;
        private readonly OvertimeEntriesController _controller;

        public OvertimeEntriesTests()
        {
            _repoMock = new Mock<IOvertimeEntriesRepository>();
            _controller = new OvertimeEntriesController(_repoMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_ReturnOk_When_ValidRequest()
        {
            // Arrange
            var request = new AddOvertimeEntryRequest
            {
                EmployeeId = 1,
                WorkDate = new DateTime(2025, 11, 1),
                Hours = 5
            };

            var response = new BaseSpResponseModel
            {
                IsSuccess = true,
                Message = "Overtime entry added successfully"
            };

            // Act
            _repoMock.Setup(r => r.AddAsync(It.IsAny<AddOvertimeEntryRequest>()))
                     .ReturnsAsync(response);

            var result = await _controller.AddAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaseSpResponseModel>(okResult.Value);
            returnValue.IsSuccess.Should().BeTrue();
            returnValue.Message.Should().Contain("successfully");
        }

        [Fact]
        public async Task UpdateAsync_Should_ReturnOk_When_ValidRequest()
        {
            // Arrange
            var request = new UpdateOvertimeEntryRequest
            {
                OvertimeId = 1,
                WorkDate = new DateTime(2025, 11, 2),
                Hours = 6,
                EmployeeId = 1
            };

            var response = new BaseSpResponseModel
            {
                IsSuccess = true,
                Message = "Updated successfully"
            };

            // Act
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<UpdateOvertimeEntryRequest>()))
                     .ReturnsAsync(response);

            var result = await _controller.UpdateAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaseSpResponseModel>(okResult.Value);

            returnValue.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_Should_ReturnOk_When_RecordExists()
        {
            // Arrange
            var request = new DeleteOvertimeEntryRequest { OvertimeId = 1 };

            var response = new BaseSpResponseModel
            {
                IsSuccess = true,
                Message = "Deleted successfully"
            };

            // Act
            _repoMock.Setup(r => r.DeleteAsync(It.IsAny<DeleteOvertimeEntryRequest>()))
                     .ReturnsAsync(response);

            var result = await _controller.DeleteAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaseSpResponseModel>(okResult.Value);

            returnValue.IsSuccess.Should().BeTrue();
            returnValue.Message.Should().Be("Deleted successfully");
        }

        [Fact]
        public async Task GetSummaryAsync_Should_ReturnList_When_DataExists()
        {
            // Arrange
            var model = new GetOvertimeEntrySummaryRequest { Month = 11, Year = 2025 };

            var summaryList = new List<OvertimeEntriesSummaryResponse>
            {
                new OvertimeEntriesSummaryResponse { FirstName = "Görkem", LastName = "Tok", TotalHours = 12 },
                new OvertimeEntriesSummaryResponse { FirstName = "Patik", LastName = "Tok", TotalHours = 8 }
            };

            // Act
            _repoMock.Setup(r => r.Summary(It.IsAny<DateTime>())).ReturnsAsync(summaryList);

            var result = await _controller.GetSummaryAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<OvertimeEntriesSummaryResponse>>(okResult.Value);

            returnValue.Should().HaveCount(2);
            returnValue[0].TotalHours.Should().Be(12);
        }

        [Fact]
        public async Task GetHistoryAsync_Should_ReturnList_When_ValidEmployeeId()
        {
            // Arrange
            var model = new GetOvertimeEntryHistoriesRequest
            {
                Year = 2025,
                Month = 11,
                EmployeeId = 1
            };

            var historyList = new List<OvertimeEntriesHistoryResponse>
            {
                new OvertimeEntriesHistoryResponse { WorkDate = new DateTime(2025, 11, 1), Hours = 3 },
                new OvertimeEntriesHistoryResponse { WorkDate = new DateTime(2025, 11, 2), Hours = 2 }
            };

            // Act
            _repoMock.Setup(r => r.GetHistoryAsync(It.IsAny<DateTime>(), It.IsAny<int>()))
                     .ReturnsAsync(historyList);

            var result = await _controller.GetHistoryAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<OvertimeEntriesHistoryResponse>>(okResult.Value);

            returnValue.Should().HaveCount(2);
            returnValue[0].Hours.Should().Be(3);
        }
    }
}
