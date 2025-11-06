using EmployeePayroll.Api.Controllers;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Models.Shared;
using EmployeePayroll.Api.Models.WorkEntries;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeePayroll.Tests.UnitTests
{
    public class WorkEntriesTests
    {
        private readonly Mock<IWorkEntriesRepository> _repoMock;
        private readonly WorkEntriesController _controller;

        public WorkEntriesTests()
        {
            _repoMock = new Mock<IWorkEntriesRepository>();
            _controller = new WorkEntriesController(_repoMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_ReturnOk_When_ValidRequest()
        {
            // Arrange
            var request = new AddWorkEntriesRequest
            {
                EmployeeId = 1,
                WorkDate = new DateTime(2025, 11, 1)
            };

            var response = new BaseSpResponseModel
            {
                IsSuccess = true,
                Message = "Work entry added successfully"
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<AddWorkEntriesRequest>()))
                     .ReturnsAsync(response);

            // Act
            var result = await _controller.AddAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaseSpResponseModel>(okResult.Value);

            returnValue.IsSuccess.Should().BeTrue();
            returnValue.Message.Should().Contain("successfully");
        }

        [Fact]
        public async Task DeleteAsync_Should_ReturnOk_When_ValidId()
        {
            // Arrange
            var request = new DeleteWorkEntryRequest { WorkEntryId = 1 };

            var response = new BaseSpResponseModel
            {
                IsSuccess = true,
                Message = "Deleted successfully"
            };

            _repoMock.Setup(r => r.DeleteAsync(It.IsAny<DeleteWorkEntryRequest>()))
                     .ReturnsAsync(response);

            // Act
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
            var model = new GetWorkEntriesSummaryRequest { Month = 11, Year = 2025 };

            var summaryList = new List<WorkEntriesSummaryResponse>
            {
                new WorkEntriesSummaryResponse { FirstName = "Görkem", LastName = "Tok", TotalWorkDaysCount = 20 },
                new WorkEntriesSummaryResponse { FirstName = "Patik", LastName = "Tok", TotalWorkDaysCount = 18 }
            };

            _repoMock.Setup(r => r.SummaryAsync(It.IsAny<DateTime>()))
                     .ReturnsAsync(summaryList);

            // Act
            var result = await _controller.GetSummaryAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<WorkEntriesSummaryResponse>>(okResult.Value);

            returnValue.Should().HaveCount(2);
            returnValue[0].TotalWorkDaysCount.Should().Be(20);
        }

        [Fact]
        public async Task GetHistoryAsync_Should_ReturnList_When_EmployeeIdIsValid()
        {
            // Arrange
            var model = new GetWorkEntriesHistoryRequest
            {
                Year = 2025,
                Month = 11,
                EmployeeId = 1
            };

            var historyList = new List<WorkEntriesHistoryResponse>
            {
                new WorkEntriesHistoryResponse { WorkDate = new DateTime(2025, 11, 1) },
                new WorkEntriesHistoryResponse { WorkDate = new DateTime(2025, 11, 2) }
            };

            _repoMock.Setup(r => r.HistoryAsync(It.IsAny<DateTime>(), It.IsAny<int>()))
                     .ReturnsAsync(historyList);

            // Act
            var result = await _controller.GetHistoryAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<WorkEntriesHistoryResponse>>(okResult.Value);

            returnValue.Should().HaveCount(2);
            returnValue[0].WorkDate.Should().Be(new DateTime(2025, 11, 1));
        }
    }
}
