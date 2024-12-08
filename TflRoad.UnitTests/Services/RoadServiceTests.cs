using Moq;
using System.Net;
using TflRoad.Application.Interfaces;
using TflRoad.Application.Models.Api;
using TflRoad.Application.Models.Api.RoadResults;
using TflRoad.Infrastructure.Services;

namespace TflRoad.UnitTests.Services
{
    public class RoadServiceTests
    {
        private readonly Mock<IRoadApi> _mockRoadApi;
        private readonly RoadService _roadService;

        public RoadServiceTests()
        {
            _mockRoadApi = new Mock<IRoadApi>();
            _roadService = new RoadService(_mockRoadApi.Object);
        }

        [Fact]
        public async Task GetRoadByIdAsync_ShouldReturnSuccess_WhenRoadIdIsValid()
        {
            // Arrange
            const string roadId = "a2";
            ApiResponse<RoadCorridor> apiResponse = ApiResponse<RoadCorridor>.Success(new RoadCorridor
            {
                DisplayName = "A2",
                StatusSeverity = "Good",
                StatusSeverityDescription = "No issues"
            });

            _mockRoadApi.Setup(api => api.GetRoadByIdAsync(roadId))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _roadService.GetRoadByIdAsync(roadId);

            // Assert
            Assert.True(result.IsSuccess);
            const string SuccessResponse =
                """
                The status of the A2 is as follows
                    Road Status is Good
                    Road Status Description is No issues
                """;
            Assert.Equal(SuccessResponse, result.Value);
        }

        [Fact]
        public async Task GetRoadByIdAsync_ShouldReturnFailure_WhenRoadIdIsInvalid()
        {
            // Arrange
            const string roadId = "InvalidRoad";
            _mockRoadApi.Setup(api => api.GetRoadByIdAsync(roadId))
                .ReturnsAsync(ApiResponse<RoadCorridor>.Failure(new ApiError { HttpStatusCode = HttpStatusCode.NotFound }));

            // Act
            var result = await _roadService.GetRoadByIdAsync(roadId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("InvalidRoad is not a valid road", result.ErrorMessage);
        }

        [Fact]
        public async Task GetRoadByIdAsync_ShouldReturnFailure_WhenApiCallFails()
        {
            // Arrange
            const string ApiIssueMessage = "test api issue";
            const string roadId = "apiIssueRoadId";

            _mockRoadApi.Setup(api => api.GetRoadByIdAsync(roadId))
                .ReturnsAsync(ApiResponse<RoadCorridor>.Failure(new ApiError { HttpStatusCode = HttpStatusCode.NotImplemented, Message = ApiIssueMessage }));

            // Act
            var result = await _roadService.GetRoadByIdAsync(roadId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ApiIssueMessage, result.ErrorMessage);
        }
    }
}
