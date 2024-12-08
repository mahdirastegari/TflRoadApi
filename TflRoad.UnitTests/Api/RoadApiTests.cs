using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using TflRoad.Infrastructure.Api;
using TflRoad.Application.Models.Api;
using TflRoad.Application.Models.Api.RoadResults;
using TflRoad.Application.Exceptions;

namespace TflRoad.UnitTests.Api
{
    public class RoadApiTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly RoadApi _roadApi;

        public RoadApiTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.tfl.gov.uk")
            };
            _roadApi = new RoadApi(_httpClient);
        }

        [Fact]
        public async Task GetRoadByIdAsync_ShouldReturnSuccess_WhenRoadExists()
        {
            // Arrange
            var roadCorridor = new RoadCorridor
            {
                Id = "A2",
                DisplayName = "A2 Road",
                Url = "Road url",
                StatusSeverity = "Status Severity",
                StatusSeverityDescription = "Status Severity Description",
                Group = "Group",
                Bounds = "Bounds",
                Envelope = "Envelope",
                StatusAggregationStartDate = new DateTime(2024, 12, 06),
                StatusAggregationEndDate = new DateTime(2024, 12, 09)
            };
            SetupHttpHandler(HttpStatusCode.OK, JsonContent.Create(new[] { roadCorridor }));

            // Act
            var result = await _roadApi.GetRoadByIdAsync("A2");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.IsSuccess);
            Assert.Equal(roadCorridor.Id, result.SuccessData.Id);
            Assert.Equal(roadCorridor.DisplayName, result.SuccessData.DisplayName);
            Assert.Equal(roadCorridor.Url, result.SuccessData.Url);
            Assert.Equal(roadCorridor.StatusSeverity, result.SuccessData.StatusSeverity);
            Assert.Equal(roadCorridor.StatusSeverityDescription, result.SuccessData.StatusSeverityDescription);
            Assert.Equal(roadCorridor.Group, result.SuccessData.Group);
            Assert.Equal(roadCorridor.Bounds, result.SuccessData.Bounds);
            Assert.Equal(roadCorridor.Envelope, result.SuccessData.Envelope);
            Assert.Equal(roadCorridor.StatusAggregationStartDate, result.SuccessData.StatusAggregationStartDate);
            Assert.Equal(roadCorridor.StatusAggregationEndDate, result.SuccessData.StatusAggregationEndDate);
        }

        [Fact]
        public async Task GetRoadByIdAsync_ShouldReturnFailure_WhenRoadDoesNotExist()
        {
            // Arrange
            var apiError = new ApiError
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                HttpStatus = "NotFound",
                ExceptionType = "Exception Type",
                RelativeUri = "Relative URI",
                TimestampUtc = new DateTime(2024, 12, 06),
                Message = "The road ID is not recognized."
            };
            SetupHttpHandler(HttpStatusCode.NotFound, JsonContent.Create(apiError));

            // Act
            var result = await _roadApi.GetRoadByIdAsync("InvalidRoadId");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(apiError.Message, result.FailureData.Message);
            Assert.Equal(apiError.HttpStatusCode, result.FailureData.HttpStatusCode);
            Assert.Equal(apiError.HttpStatus, result.FailureData.HttpStatus);
            Assert.Equal(apiError.ExceptionType, result.FailureData.ExceptionType);
            Assert.Equal(apiError.RelativeUri, result.FailureData.RelativeUri);
            Assert.Equal(apiError.TimestampUtc, result.FailureData.TimestampUtc);
        }

        [Fact]
        public async Task GetRoadByIdAsync_ShouldThrowApiException_WhenResponseCannotBeDeserialized()
        {
            // Arrange
            SetupHttpHandler(HttpStatusCode.OK, new StringContent("Invalid JSON Format"));

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(async () => await _roadApi.GetRoadByIdAsync("A2"));
        }

        private void SetupHttpHandler(HttpStatusCode statusCode, HttpContent content)
        {
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(statusCode)
                {
                    Content = content
                });
        }
    }
}
