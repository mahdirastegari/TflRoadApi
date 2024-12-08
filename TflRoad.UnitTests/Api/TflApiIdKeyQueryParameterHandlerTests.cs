using System.Net;
using Moq;
using Moq.Protected;
using TflRoad.Infrastructure.Api;

namespace TflRoad.UnitTests.Api
{
    /// <summary>
    /// Unit tests for <see cref="TflApiIdKeyQueryParameterHandler"/>
    /// </summary>
    public class TflApiIdKeyQueryParameterHandlerTests
    {
        private const string AppId = "testAppId";
        private const string AppKey = "testAppKey";
        private HttpClient _httpClient;

        public TflApiIdKeyQueryParameterHandlerTests()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = request
                    };
                })
                .Verifiable();

            var tflHandler = new TflApiIdKeyQueryParameterHandler(AppId, AppKey)
            {
                InnerHandler = handlerMock.Object
            };
            _httpClient = new HttpClient(tflHandler);
        }

        [Fact]
        public async Task SendAsync_ShouldAddApiIdAndKeyToQueryParameters()
        {
            // Arrange
            const string requestUri = "https://api.tfl.gov.uk/Road";

            // Act
            var response = await _httpClient.GetAsync(requestUri);

            // Assert
            Assert.Equal("https://api.tfl.gov.uk/Road?app_id=testAppId&app_key=testAppKey", response.RequestMessage.RequestUri.ToString());
        }

        [Fact]
        public async Task SendAsync_ShouldPreserveExistingQueryParameters()
        {
            // Arrange
            const string requestUri = "https://api.tfl.gov.uk/Road?existing_param=existingValue";

            // Act
            var response = await _httpClient.GetAsync(requestUri);

            // Assert
            string expectedUri = "https://api.tfl.gov.uk/Road?existing_param=existingValue&app_id=testAppId&app_key=testAppKey";
            Assert.Equal(expectedUri, response.RequestMessage.RequestUri.ToString());
        }
    }
}
